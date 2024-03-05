using UnityEngine;

namespace Unite.ObjectiveSystem
{
    /// <summary>
    /// Wrapper class for the ObjectiveSO scriptable object,
    /// used to manage a change in state of the objective and to prevent
    /// mutation of data within the scriptable object.
    ///
    /// <seealso cref="ObjectiveSO"/>
    /// </summary>
    public class Objective
    {
        private ObjectiveSO objectiveData;
        private ObjectiveState currentState;
        private int currentTaskIndex;

        public ObjectiveSO ObjectiveData => objectiveData;

        public Objective(ObjectiveSO objectiveData)
        {
            this.objectiveData = objectiveData;
            currentState = ObjectiveState.NotStarted;
            currentTaskIndex = 0;
        }

        public void MoveToNextTask()
        {
            currentTaskIndex++;
        }

        public void SetState(ObjectiveState newState)
        {
            currentState = newState;
        }

        public bool CurrentTaskExists()
        {
            return (currentTaskIndex < objectiveData.Tasks.Count);
        }

        public void InstantiateCurrentTask(Transform parent)
        {
            ObjectiveTask taskPrefab = GetCurrentTaskPrefab();
            if (taskPrefab == null) return;

            ObjectiveTask task = Object.Instantiate(taskPrefab, parent);
            task.InitializeTask(objectiveData.name);
        }

        public ObjectiveTask GetCurrentTaskPrefab()
        {
            ObjectiveTask taskPrefab = null;
            if (CurrentTaskExists())
            {
                taskPrefab = objectiveData.Tasks[currentTaskIndex];
            }
            else
            {
                Debug.LogWarning($"tried to get objective task prefab," +
                                 $" but the current task index was out of range." +
                                 $" Objective: {objectiveData.name}, currentTaskIndex: {currentTaskIndex}");
            }

            return taskPrefab;
        }
    }
}