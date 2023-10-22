using System;
using UnityEngine;

namespace Unite
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

        public event Action<bool> ToggleTimeStop;

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
            ToggleTimeStop?.Invoke(isTimeStopped);
        }
    }
}

