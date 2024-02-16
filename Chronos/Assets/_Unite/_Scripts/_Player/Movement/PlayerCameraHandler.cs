using Unite.Core.Input;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerCameraHandler : MonoBehaviour
    {
        [Header("Sensitivity Settings")]
        [SerializeField]
        private float startingXSensitivity;
        [SerializeField]
        private float startingYSensitivity;

        [Header("Orientation Config")] 
        [SerializeField]
        private Transform orientation;

        [Header("X Rotation Clamp Settings")] 
        [SerializeField]
        private float minXRotation = -90f;
        [SerializeField]
        private float maxXRotation = 90f;

        private float xSens;
        private float ySens;

        private float xRotation;
        private float yRotation;

        private void Awake()
        {
            xSens = startingXSensitivity;
            ySens = startingYSensitivity;
        }

        private void Update()
        {
            Vector2 lookVector = InputManager.Instance.GetLookVectorNormalized();

            yRotation += lookVector.x * Time.deltaTime * xSens;
            xRotation -= lookVector.y * Time.deltaTime * ySens;
            xRotation = Mathf.Clamp(xRotation, minXRotation, maxXRotation);
            
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}