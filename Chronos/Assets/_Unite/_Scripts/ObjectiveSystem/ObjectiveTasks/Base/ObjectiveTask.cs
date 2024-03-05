using Unite.EventSystem;
using UnityEngine;

namespace Unite.ObjectiveSystem
{
    /// <summary>
    /// Abstract class that represents a single task within an objective.
    /// Concrete implementations will handle the logic behind the completion of different tasks.
    /// <seealso cref="EventListenerObjectiveTask"/>
    ///
    /// Stored as a prefab and instantiated when the objective is either started or advanced.
    /// </summary>
    public abstract class ObjectiveTask : MonoBehaviour
    {
        [SerializeField]
        [TextArea(2,10)]
        private string taskDescription;
        
        [SerializeField]
        private StringEvent onTaskComplete;
        
        private string parentObjectiveName;
        private bool isCompleted;

        public string Description => taskDescription;

        public void InitializeTask(string objectiveName)
        {
            parentObjectiveName = objectiveName;
        }

        public virtual void CompleteTask()
        {
            if (isCompleted) return;

            isCompleted = true;
            
            onTaskComplete.Raise(parentObjectiveName);
            
            Destroy(gameObject);
        }
    }
}