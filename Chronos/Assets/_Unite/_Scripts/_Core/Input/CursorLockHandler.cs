using UnityEngine;

namespace Unite.Core.Input
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

