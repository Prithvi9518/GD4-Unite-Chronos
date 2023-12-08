using UnityEngine;

namespace Unite.Player
{
    public class CursorLockHandler : MonoBehaviour
    {
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.visible = false;
            }
        }
    }
}

