using Unite.EventSystem;
using UnityEngine;

namespace Unite.Managers
{
    public class TimeScaleManager : MonoBehaviour
    {
        [SerializeField]
        private GameEvent onPause;
        [SerializeField]
        private GameEvent onUnpause;

        [SerializeField]
        [Range(0,1f)]
        private float slowdownTimeScale;
        
        public void Pause()
        {
            onPause.Raise();
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            onUnpause.Raise();
            Time.timeScale = 1;
        }

        public void Slowdown()
        {
            Time.timeScale = slowdownTimeScale;
        }

        public void EndSlowdown()
        {
            Time.timeScale = 1;
        }
    }
}