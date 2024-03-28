using TMPro;
using Unite.Core.Input;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.UI
{
    public class YesNoUI : MonoBehaviour
    {
        [SerializeField]
        private Transform container;

        [SerializeField]
        private TextMeshProUGUI headerText;

        private GameEvent onYes;
        private GameEvent onNo;

        private void Awake()
        {
            container.gameObject.SetActive(false);
        }
        
        public void ShowUI()
        {
            container.gameObject.SetActive(true);
            
            InputManager.Instance.SwitchToUIActionMap();
            CursorLockHandler.Instance.ShowAndUnlockCursor();
        }

        public void HideUI()
        {
            container.gameObject.SetActive(false);
            CursorLockHandler.Instance.HideAndLockCursor();
        }

        public void OnYes()
        {
            if (onYes == null) return;
            onYes.Raise();
        }

        public void OnNo()
        {
            if (onNo == null) return;
            onNo.Raise();
            InputManager.Instance.SwitchToDefaultActionMap();
        }

        public void SetContext(YesNoUIContext ctx)
        {
            headerText.text = ctx.HeaderText;
            onYes = ctx.OnYes;
            onNo = ctx.OnNo;
        }
    }
}