using Unite.Core.Input;
using Unite.Managers;
using UnityEngine;

namespace Unite.UI
{
    public class EndScreenUI : MonoBehaviour
    {
        [SerializeField] private Transform panel;

        private void Awake()
        {
            panel.gameObject.SetActive(false);
        }

        public void ShowEndScreen()
        {
            InputManager.Instance.SwitchToUIActionMap();
            panel.gameObject.SetActive(true);
            CursorLockHandler.Instance.ShowAndUnlockCursor();
        }

        public void HideEndScreen()
        {
            InputManager.Instance.SwitchToDefaultActionMap();
            panel.gameObject.SetActive(false);
            CursorLockHandler.Instance.HideAndLockCursor();
        }

        public void OnBackToMainMenu()
        {
            GameManager.Instance.SwitchToMainMenu();
        }
    }
}