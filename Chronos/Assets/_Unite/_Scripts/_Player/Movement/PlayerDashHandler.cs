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
        
        public PlayerDashHandler(PlayerController controller)
        {
            orientation = controller.Orientation;
            playerCam = controller.PlayerCamera.transform;
            rb = controller.PlayerRigidbody;
            movementData = controller.MovementData;
            playerController = controller;
        }
    }
}