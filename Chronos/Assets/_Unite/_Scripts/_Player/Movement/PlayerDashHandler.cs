using System.Collections;
using Unite.Core.Input;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerDashHandler
    {
        private Transform orientation;
        private Transform playerCam;
        private Rigidbody rb;
        private PlayerMovementData movementData;
        private PlayerController playerController;

        private float dashCooldownTimer;

        private Vector3 delayedForceToApply;
        
        public PlayerDashHandler(PlayerController controller)
        {
            orientation = controller.Orientation;
            playerCam = controller.PlayerCamera.transform;
            rb = controller.PlayerRigidbody;
            movementData = controller.MovementData;
            playerController = controller;
        }

        public void DoUpdate()
        {
            if (dashCooldownTimer > 0)
                dashCooldownTimer -= Time.deltaTime;
        }

        public void Dash()
        {
            if (dashCooldownTimer > 0) return;
            else dashCooldownTimer = movementData.DashCooldown;
            
            playerController.IsDashing = true;
            playerController.MaxYSpeed = movementData.MaxDashYSpeed;

            Transform forwardTransform;
            if (movementData.DashUseCameraForward)
                forwardTransform = playerCam.transform;
            else
                forwardTransform = orientation;

            Vector3 direction = GetDirectionNormalized(forwardTransform);
            
            Vector3 forceToApply = direction * movementData.DashForce +
                                   orientation.up * movementData.DashUpwardForce;

            if (movementData.DashDisableGravity)
                rb.useGravity = false;

            delayedForceToApply = forceToApply;
            playerController.StartCoroutine(DelayedDashForceCoroutine());
            
            playerController.StartCoroutine(ResetDashCoroutine());
        }

        private IEnumerator DelayedDashForceCoroutine()
        {
            yield return new WaitForSeconds(movementData.DashForceDelay);
            
            if (movementData.DashResetVelocity)
                rb.velocity = Vector3.zero;
            rb.AddForce(delayedForceToApply, ForceMode.Impulse);
        }

        private IEnumerator ResetDashCoroutine()
        {
            yield return new WaitForSeconds(movementData.DashDuration);
            ResetDash();
        }

        private void ResetDash()
        {
            playerController.IsDashing = false;
            playerController.MaxYSpeed = 0;
            
            if (movementData.DashDisableGravity)
                rb.useGravity = true;
        }

        private Vector3 GetDirectionNormalized(Transform forwardTransform)
        {
            Vector2 moveVector = InputManager.Instance.GetMovementVectorNormalized();

            Vector3 direction = forwardTransform.forward;

            if (movementData.DashAllowAllDirections)
            {
                direction = forwardTransform.forward * moveVector.y + forwardTransform.right * moveVector.x;
            }

            if (moveVector.x == 0 && moveVector.y == 0)
                direction = forwardTransform.forward;

            return direction.normalized;
        }
    }
}