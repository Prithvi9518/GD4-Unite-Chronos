using Unite.EventSystem;
using Unite.Managers;
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

        private void OnEnable()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.OnBackToMainMenu += DeleteTask;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnBackToMainMenu -= DeleteTask;
        }

        private void DeleteTask()
        {
            Destroy(gameObject);
        }

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