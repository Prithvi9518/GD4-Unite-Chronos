using UnityEngine;

namespace Unite.Bootstrap
{
    /// <summary>
    /// Registers the camera with the GameManager when the camera is initialized.
    /// </summary>
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