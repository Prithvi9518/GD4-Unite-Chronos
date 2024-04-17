using UnityEngine;

namespace Unite.Core.Input
{
    /// <summary>
    /// Manager class to control the hiding/un-hiding of the mouse cursor.
    /// </summary>
    public class CursorLockHandler : MonoBehaviour
    {
        public static CursorLockHandler Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
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

