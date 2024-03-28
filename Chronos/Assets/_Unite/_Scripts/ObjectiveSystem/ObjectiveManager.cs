using System.Collections.Generic;
using Unite.ActionSystem;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.ObjectiveSystem
{
    /// <summary>
    /// Manager to handle the start and advancement of game objectives.
    /// Keeps track of all in-game objectives using an ObjectiveListSO scriptable object,
    /// and stores them in a Dictionary for quick lookup.
    /// Receives StringEvents with the ObjectiveSO's name to lookup the dictionary.
    ///
    /// <seealso cref="ObjectiveSO"/>
    /// <seealso cref="ObjectiveListSO"/>
    /// </summary>
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance { get; private set; }
        
        [SerializeField]
        private ObjectiveListSO allObjectivesList;

        [SerializeField]
        private ObjectiveEvent onObjectiveStarted;
        
        [SerializeField]
        private ObjectiveEvent onObjectiveProgressed;

        [SerializeField]
        private ObjectiveEvent onObjectiveCompleted;
        
        private Dictionary<string, Objective> objectiveMap;
        private List<Objective> activeObjectives;

        public List<Objective> ActiveObjectives => activeObjectives;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of ObjectiveManager in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
            objectiveMap = CreateObjectiveMap();
            activeObjectives = new();
        }

        public void StartObjective(string objectiveName)
        {
            Objective objective = GetObjectiveByName(objectiveName);
            objective.InstantiateCurrentTask(this.transform);
            ChangeObjectiveState(objectiveName, ObjectiveState.Started);
            
            activeObjectives.Add(objective);
            onObjectiveStarted.Raise(objective);
        }

        /// <summary>
        /// Called by an ObjectiveTask upon completion.
        /// AdvanceObjective moves on to the next task within the objective task sequence.
        /// If there are no remaining tasks, then the objective is completed.
        /// </summary>
        /// <param name="objectiveName"></param>
        public void AdvanceObjective(string objectiveName)
        {
            Objective objective = GetObjectiveByName(objectiveName);
            objective.MoveToNextTask();

            if (objective.CurrentTaskExists())
            {
                objective.InstantiateCurrentTask(this.transform);
                onObjectiveProgressed.Raise(objective);
            }
            else
            {
                FinishObjective(objectiveName);
            }
        }

        public bool IsObjectiveActive(ObjectiveSO objectiveData)
        {
            Objective objective = GetObjectiveByName(objectiveData.name);
            if (objective == null) return false;

            return objective.CurrentState == ObjectiveState.Started;
        }

        private void FinishObjective(string objectiveName)
        {
            Objective objective = GetObjectiveByName(objectiveName);
            ChangeObjectiveState(objectiveName, ObjectiveState.Complete);

            var onCompleteEvent = objective.ObjectiveData.OnCompleteEvent;
            if (onCompleteEvent != null)
                onCompleteEvent.Raise();

            foreach (var action in objective.ObjectiveData.ActionsOnComplete)
            {
                ActionExecutionManager.Instance.ExecuteAction(action);
            }
            
            activeObjectives.Remove(objective);
            onObjectiveCompleted.Raise(objective);
        }

        private Dictionary<string, Objective> CreateObjectiveMap()
        {
            Dictionary<string, Objective> map = new();
            foreach (var objectiveData in allObjectivesList.Objectives)
            {
                if (map.ContainsKey(objectiveData.name))
                {
                    Debug.LogWarning($"Objective name already exists in the map: {objectiveData.name}");
                }
                map.Add(objectiveData.name, new Objective(objectiveData));
            }

            return map;
        }

        private Objective GetObjectiveByName(string objectiveName)
        {
            Objective objective = objectiveMap[objectiveName];
            if (objective == null)
            {
                Debug.LogError("Objective name not found in the map: " + objectiveName);
            }

            return objective;
        }

        private void ChangeObjectiveState(string objectiveName, ObjectiveState state)
        {
            Objective objective = GetObjectiveByName(objectiveName);
            objective.SetState(state);
        }
    }
}