using Unite.Core.Input;
using UnityEngine;
using DG.Tweening;

namespace Unite.Player
{
    public class PlayerCameraHandler : MonoBehaviour
    {
        // [SerializeField] 
        // private Camera cam;
        
        [Header("Sensitivity Settings")]
        [SerializeField]
        private float xSensitivity;
        [SerializeField]
        private float ySensitivity;
        [SerializeField]
        private float changeThreshold = 0.01f;

        [Header("Orientation Config")] 
        [SerializeField]
        private Transform orientation;
        [SerializeField]
        private Transform cinemachineTarget;

        [Header("X Rotation Clamp Settings")] 
        [SerializeField]
        private float minXRotation = -90f;
        [SerializeField]
        private float maxXRotation = 90f;
        
        private Camera cam;

        private float xRotation;
        private float yRotation;

        private Quaternion targetCameraRotation;
        private Quaternion targetOrientationRotation;

        private float defaultFOV;

        private bool initializedCamera;

        public Camera PlayerCamera => cam;
        public float DefaultFOV => defaultFOV;

        // private void Start()
        // {
        //     Cursor.lockState = CursorLockMode.Locked;
        //     Cursor.visible = false;
        // }

        private void Update()
        {
            if (!initializedCamera) return;
            
            Vector2 lookVector = InputManager.Instance.GetLookVectorNormalized();
            if (lookVector.sqrMagnitude < changeThreshold) return;

            float mouseX = lookVector.x * xSensitivity;
            float mouseY = lookVector.y * ySensitivity;
            
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);
            
            targetCameraRotation = Quaternion.Euler(xRotation, yRotation, 0);
            targetOrientationRotation = Quaternion.Euler(0, yRotation, 0);
            
            cam.transform.rotation = targetCameraRotation;
            orientation.rotation = targetOrientationRotation;
        }

        public void DoFov(float endValue, float endDuration)
        {
            cam.DOFieldOfView(endValue, endDuration);
        }

        public void InitializeCamera(Camera playerCamera)
        {
            cam = playerCamera;
            defaultFOV = cam.fieldOfView;
            initializedCamera = true;
        }
    }
}