using System.Collections.Generic;
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

        // Actions that are triggered when the objective is completed
        [SerializeField]
        private GameEvent onComplete;
        
        public string ObjectiveDescription => objectiveDescription;
        public List<ObjectiveTask> Tasks => tasks;

        public GameEvent OnComplete => onComplete;
    }
}