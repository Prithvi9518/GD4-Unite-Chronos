using UnityEngine;

namespace Unite.Bootstrap
{
    public class PlayerCameraBootstrap : MonoBehaviour
    {
        [SerializeField]
        private Camera cam;

        [SerializeField]
        private Transform weaponsHolder;

        private void Start()
        {
            Managers.GameManager.Instance.InitializeCamera(cam, weaponsHolder);
        }
    }
}