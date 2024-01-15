using UnityEngine;

namespace Unite.Player
{
    public class CursorLockHandler : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

