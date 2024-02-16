using Unite.Core.Input;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerCameraHandler : MonoBehaviour
    {
        [Header("Sensitivity Settings")]
        [SerializeField]
        private float xSensitivity;
        [SerializeField]
        private float ySensitivity;

        [Header("Orientation Config")] 
        [SerializeField]
        private Transform orientation;

        [Header("X Rotation Clamp Settings")] 
        [SerializeField]
        private float minXRotation = -90f;
        [SerializeField]
        private float maxXRotation = 90f;

        [Header("Smoothing")]
        [SerializeField]
        private float smoothingFactor;
        
        private float xRotation;
        private float yRotation;

        private void Start()
        {
            yRotation = 180f;
        }

        private void Update()
        {
            Vector2 lookVector = InputManager.Instance.GetLookVectorNormalized();

            float mouseX = lookVector.x * xSensitivity;
            float mouseY = lookVector.y * ySensitivity;
            
            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);
            
            Quaternion targetRotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.rotation = targetRotation;
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}