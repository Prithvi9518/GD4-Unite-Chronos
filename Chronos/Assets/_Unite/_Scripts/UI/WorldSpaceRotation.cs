using UnityEngine;

namespace Unite.UI
{
    public class WorldSpaceRotation : MonoBehaviour
    {
        private Camera cam;

        private void Update()
        {
            if (cam == null)
            {
                cam = Camera.main;
            }

            if (cam == null) return;
            transform.parent.rotation = cam.transform.rotation;
        }
    }
}