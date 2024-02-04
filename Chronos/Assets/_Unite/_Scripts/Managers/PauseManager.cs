using Unite.Core.Input;
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
            InputManager.Instance.SwitchToUIActionMap();
            onPause.Raise();
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            InputManager.Instance.SwitchToDefaultActionMap();
            onUnpause.Raise();
            Time.timeScale = 1;
        }
    }
}