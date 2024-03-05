using System.Collections;
using UnityEngine;

namespace Unite.ActionSystem
{
    /// <summary>
    /// Singleton manager responsible for the execution of ActionSO objects.
    /// If an action has no delay, then it is immediately executed.
    /// If an action has a delay, then a coroutine is started to perform the delayed execution.
    ///
    /// <seealso cref="ActionSO"/>
    /// </summary>
    public class ActionExecutionManager : MonoBehaviour
    {
        public static ActionExecutionManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of ActionExecutionManager present in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
        }

        public void ExecuteAction(ActionSO action)
        {
            if (!action.HasDelay)
            {
                action.ExecuteAction();
                return;
            }

            StartCoroutine(ExecuteActionWithDelay(action));
        }

        private IEnumerator ExecuteActionWithDelay(ActionSO action)
        {
            yield return new WaitForSeconds(action.DelayInSeconds);
            action.ExecuteAction();
        }
    }
}