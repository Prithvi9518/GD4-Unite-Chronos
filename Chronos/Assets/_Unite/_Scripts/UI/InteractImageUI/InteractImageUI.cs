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
            InputManager.Instance.SwitchToViewImageActionMap();
        }
        
        private void OnHideUI()
        {
            InputManager.Instance.SwitchToDefaultActionMap();
        }
    }
}