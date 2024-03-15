using Unite.EventSystem;
using UnityEngine;

namespace Unite.TimeStop
{
    public class TimeStopManager : MonoBehaviour
    {
        public static TimeStopManager Instance { get; private set; }

        [SerializeField]
        private BoolGameEvent onToggleTimeStop;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }

        public void TriggerTimeStop(bool isTimeStopped)
        {
            onToggleTimeStop.Raise(isTimeStopped);
        }
    }
}