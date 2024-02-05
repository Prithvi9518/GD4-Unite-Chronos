using UnityEngine;

namespace Unite.UI
{
    public class WorldSpaceRotation : MonoBehaviour
    {
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            transform.parent.rotation = cam.transform.rotation;
        }
    }
}