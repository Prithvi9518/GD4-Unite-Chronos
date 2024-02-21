using Unite.Core.Input;
using UnityEngine;

namespace Unite.Player
{
    public class WeaponBobAndSway : MonoBehaviour
    {
        [Header("Toggle Settings")]
        [SerializeField]
        private bool toggleSwayPosition;
        [SerializeField]
        private bool toggleSwayRotation;
        [SerializeField]
        private bool toggleBobOffset;
        [SerializeField]
        private bool toggleBobRotation;
        
        [Header("Sway Position Settings")] 
        [SerializeField]
        private float step = 0.01f;
        [SerializeField]
        private float maxStepDistance = 0.06f;

        [Header("Sway Rotation Settings")] 
        [SerializeField]
        private float rotationStep = 4f;
        [SerializeField]
        private float maxRotationStep = 5f;
        
        [Header("Bobbing Position Settings")] 
        [SerializeField]
        private Vector3 travelLimit = Vector3.one * 0.025f;
        [SerializeField]
        private Vector3 bobLimit = Vector3.one * 0.01f;

        [Header("Bobbing Rotation Settings")] 
        [SerializeField]
        private Vector3 bobRotationMultiplier;
        
        [Header("Smoothing")]
        [SerializeField]
        private float positionSmoothing = 10f;
        [SerializeField]
        private float rotationSmoothing = 12f;
        
        private Vector3 swayPos;
        private Vector3 swayEulerRotation;

        private Vector3 bobPosition;
        private Vector3 bobEulerRotation;

        private float bobSpeedCurve;
        private float BobCurveSin => Mathf.Sin(bobSpeedCurve);
        private float BobCurveCos => Mathf.Cos(bobSpeedCurve);

        private Vector2 moveInput;
        private Vector2 lookInput;

        private PlayerController controller;
        private bool isControllerSetupDone;

        public void PerformSetup(PlayerController playerController)
        {
            controller = playerController;
            isControllerSetupDone = true;
        }

        private void Update()
        {
            GetInput();

            SwayPosition();
            SwayRotation();
            
            BobOffset();
            BobRotation();
            
            CompositePositionRotation();
        }

        private void GetInput()
        {
            moveInput = InputManager.Instance.GetMovementVectorNormalized();
            lookInput = InputManager.Instance.GetLookVectorNormalized();
        }

        private void SwayPosition()
        {
            if (!toggleSwayPosition)
            {
                swayPos = Vector3.zero;
                return;
            }
            
            Vector3 invertLook = lookInput * (step * -1);
            
            invertLook.x = Mathf.Clamp(invertLook.x, -maxStepDistance, maxStepDistance);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxStepDistance, maxStepDistance);
            
            swayPos = invertLook;
        }

        private void SwayRotation()
        {
            if (!toggleSwayRotation)
            {
                swayEulerRotation = Vector3.zero;
                return;
            }
            
            Vector2 invertLook = lookInput * (rotationStep * -1);

            invertLook.x = Mathf.Clamp(invertLook.x, -maxRotationStep, maxRotationStep);
            invertLook.y = Mathf.Clamp(invertLook.y, -maxRotationStep, maxRotationStep);

            swayEulerRotation = new Vector3(invertLook.y, invertLook.x, invertLook.x);
        }

        private void CompositePositionRotation()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, swayPos + bobPosition,
                Time.deltaTime * positionSmoothing);
            transform.localRotation = Quaternion.Slerp(transform.localRotation,
                Quaternion.Euler(swayEulerRotation) * Quaternion.Euler(bobEulerRotation),
                Time.deltaTime * rotationSmoothing);
        }

        private void BobOffset()
        {
            if (!isControllerSetupDone) return;
            
            bobSpeedCurve += Time.deltaTime * (controller.IsGrounded ? controller.PlayerRigidbody.velocity.magnitude : 1f) + 0.01f;
            
            if (!toggleBobOffset)
            {
                bobPosition = Vector3.zero;
                return;
            }

            bobPosition.x = (BobCurveCos*bobLimit.x*(controller.IsGrounded ? 1:0))-(moveInput.x * travelLimit.x);

            bobPosition.y = (BobCurveSin*bobLimit.y)-(controller.PlayerRigidbody.velocity.y * travelLimit.y);

            bobPosition.z = -(moveInput.y * travelLimit.z);
        }

        private void BobRotation()
        {
            if (!toggleBobRotation)
            {
                bobEulerRotation = Vector3.zero;
                return;
            }
            
            bobEulerRotation.x = (moveInput != Vector2.zero ? bobRotationMultiplier.x * (Mathf.Sin(2*bobSpeedCurve)) : bobRotationMultiplier.x * (Mathf.Sin(2*bobSpeedCurve) / 2));

            bobEulerRotation.y = (moveInput != Vector2.zero ? bobRotationMultiplier.y * BobCurveCos : 0);

            bobEulerRotation.z = (moveInput != Vector2.zero ? bobRotationMultiplier.z * BobCurveCos * moveInput.x : 0);
        }
        
        
    }
}