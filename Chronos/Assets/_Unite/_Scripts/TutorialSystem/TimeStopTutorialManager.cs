using Unite.ActionSystem;
using UnityEngine;

namespace Unite.TutorialSystem
{
    public class TimeStopTutorialManager : MonoBehaviour
    {
        [SerializeField] 
        private ActionSO[] onStartTutorialActions;

        public void StartTutorial()
        {
            foreach (var action in onStartTutorialActions)
            {
                ActionExecutionManager.Instance.ExecuteAction(action);
            }
        }
    }
}