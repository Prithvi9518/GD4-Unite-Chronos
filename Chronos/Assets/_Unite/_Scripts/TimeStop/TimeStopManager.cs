using Unite.EventSystem;
using UnityEngine;

namespace Unite.TimeStop
{
    public class TimeStopManager : MonoBehaviour
    {
        private static TimeStopManager instance;

        public static TimeStopManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<TimeStopManager>();
                }
                return instance;
            }
        }
        [SerializeField]
        private BoolGameEvent onToggleTimeStop;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void TriggerTimeStop(bool isTimeStopped)
        {
            onToggleTimeStop.Raise(isTimeStopped);
        }
    }
}