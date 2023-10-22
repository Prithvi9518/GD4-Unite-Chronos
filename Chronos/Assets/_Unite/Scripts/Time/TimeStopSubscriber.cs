using UnityEngine;

namespace Unite
{
    public abstract class TimeStopSubscriber : MonoBehaviour
    {
        protected TimeStopManager timeManager;

        private void OnEnable()
        {
            timeManager = FindObjectOfType<TimeStopManager>();
            timeManager.ToggleTimeStop += HandleTimeStopEvent;
        }

        private void OnDisable()
        {
            if (timeManager == null) return;
            timeManager.ToggleTimeStop -= HandleTimeStopEvent;
        }

        protected abstract void HandleTimeStopEvent(bool isTimeStopped);
    }
}