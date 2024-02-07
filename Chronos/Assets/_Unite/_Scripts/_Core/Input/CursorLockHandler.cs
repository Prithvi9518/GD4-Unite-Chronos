using UnityEngine;

namespace Unite.Core.Input
{
    public class CursorLockHandler : MonoBehaviour
    {
        private void Start()
        {
            HideAndLockCursor();
        }

        public void ShowAndUnlockCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void HideAndLockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

