using Unite.Core.Input;
using UnityEngine;

namespace Unite.UI.InteractImageUI
{
    public class InteractImageUI : MonoBehaviour
    {
        [SerializeField]
        private Transform mainPanel;
        
        [SerializeField] 
        private Transform newspaperUI;

        [SerializeField] 
        private Transform poemUI;

        private void Awake()
        {
            mainPanel.gameObject.SetActive(false);
        }

        public void ShowNewspaperUI()
        {
            mainPanel.gameObject.SetActive(true);
            newspaperUI.gameObject.SetActive(true);
            poemUI.gameObject.SetActive(false);
            
            OnShowUI();
        }

        public void ShowPoemUI()
        {
            mainPanel.gameObject.SetActive(true);
            newspaperUI.gameObject.SetActive(false);
            poemUI.gameObject.SetActive(true);
            
            OnShowUI();
        }

        public void HideUI()
        {
            mainPanel.gameObject.SetActive(false);
            OnHideUI();
        }

        private void OnShowUI()
        {
            if(InputManager.Instance != null)
                InputManager.Instance.SwitchToViewImageActionMap();
            if(CursorLockHandler.Instance != null)
                CursorLockHandler.Instance.ShowAndUnlockCursor();
        }
        
        private void OnHideUI()
        {
            if(InputManager.Instance != null)
                InputManager.Instance.SwitchToDefaultActionMap();
            if(CursorLockHandler.Instance != null)
                CursorLockHandler.Instance.HideAndLockCursor();
        }
    }
}