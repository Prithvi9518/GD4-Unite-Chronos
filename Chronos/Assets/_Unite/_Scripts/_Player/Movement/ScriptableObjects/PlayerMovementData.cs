using UnityEngine;

namespace Unite.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Player/Player Movement Data")]
    public class PlayerMovementData : ScriptableObject
    {
        [field: SerializeField]
        public float WalkSpeed { get; private set; }
        
        [field: SerializeField]
        public float SprintSpeed { get; private set; }
        
        [field: SerializeField]
        public float DashSpeed { get; private set; }
        
        [field: SerializeField]
        public float SpeedMultiplier { get; private set; }
        
        [field: SerializeField]
        public float SlopeSpeedMultiplier { get; private set; }
        
        [field: SerializeField]
        public float SlopeDownwardForce { get; private set; }
        
        [field: SerializeField]
        public float JumpForce { get; private set; }
        
        [field: SerializeField]
        public float JumpCooldown { get; private set; }
        
        [field: SerializeField]
        public float AirMultiplier { get; private set; }
        
        [field: SerializeField]
        public float DashForce { get; private set; }
        
        [field: SerializeField]
        public float DashForceDelay { get; private set; }
        
        [field: SerializeField]
        public float DashUpwardForce { get; private set; }
        
        [field: SerializeField]
        public float DashDuration { get; private set; }

        [field: SerializeField]
        public float DashCooldown { get; private set; }
        
        [field: SerializeField]
        public float DashSpeedChangeFactor { get; private set; }
        
        [field: SerializeField]
        public float MaxDashYSpeed { get; private set; }
        
        [field: SerializeField]
        public bool DashUseCameraForward { get; private set; }
        
        [field: SerializeField]
        public bool DashAllowAllDirections { get; private set; }
        
        [field: SerializeField]
        public bool DashDisableGravity { get; private set; }
        
        [field: SerializeField]
        public bool DashResetVelocity { get; private set; }
    }
}