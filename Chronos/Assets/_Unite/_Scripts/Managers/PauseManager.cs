using Unite.EventSystem;
using UnityEngine;

namespace Unite.Managers
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField]
        private GameEvent onPause;
        [SerializeField]
        private GameEvent onUnpause;
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
    }
}