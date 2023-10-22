using System;
using UnityEngine;

namespace Unite
{
    public class TimeStopManager : MonoBehaviour
    {
        public event Action<bool> ToggleTimeStop;

        public void TriggerTimeStop(bool isTimeStopped)
        {
            ToggleTimeStop?.Invoke(isTimeStopped);
        }
    }
}

