using UnityEngine;


namespace Unite.Core.InteractionSystem
{
    public class FPCameraRayProvider : MonoBehaviour, IRayProvider
    {
        [SerializeField]
        private Camera cam;

        private void Start()
        {
            if (cam != null) return;
            cam = Camera.main;
        }

        public Ray ProvideRay()
        {
            var camTransform = cam.transform;
            return new Ray(camTransform.position, camTransform.forward);
        }
    }
}

