using UnityEngine;


namespace Unite
{
    public class FPCameraRayProvider : MonoBehaviour, IRayProvider
    {
        [SerializeField]
        private Camera cam;

        public Ray ProvideRay()
        {
            return new Ray(cam.transform.position, cam.transform.forward);
        }
    }
}

