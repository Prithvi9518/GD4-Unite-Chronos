using System.Collections.Generic;
using Unite.ActionSystem;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.ObjectiveSystem
{
    /// <summary>
    /// Stores the data representing an in-game objective.
    ///
    /// An objective is made up of one or more tasks, which are represented by the ObjectiveTask class.
    /// <seealso cref="ObjectiveTask"/>
    /// </summary>
    [CreateAssetMenu(fileName = "ObjectiveSO", menuName = "Objective System/Objective SO")]
    public class ObjectiveSO : ScriptableObject
    {
        [SerializeField]
        private string objectiveDescription;

        [SerializeField]
        private List<ObjectiveTask> tasks;

        [SerializeField]
        private GameEvent onCompleteEvent;

        [SerializeField]
        private ActionSO[] actionsOnComplete;

        [Header("UI Related:")] 
        [SerializeField]
        private bool showSubtasks;
        
        public string ObjectiveDescription => objectiveDescription;
        public List<ObjectiveTask> Tasks => tasks;

        public GameEvent OnCompleteEvent => onCompleteEvent;
        public ActionSO[] ActionsOnComplete => actionsOnComplete;
        public bool ShowSubtasks => showSubtasks;
    }
}