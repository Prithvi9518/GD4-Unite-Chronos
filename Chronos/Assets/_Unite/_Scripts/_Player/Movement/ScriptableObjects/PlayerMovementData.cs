using UnityEngine;

namespace Unite.Player
{
    /// <summary>
    /// Stores all variables related to player movement (walk, run, jump, dash)
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerMovementData", menuName = "Player/Player Movement Data")]
    public class PlayerMovementData : ScriptableObject
    {
        // Speeds
        [field: SerializeField]
        public float SlowWalkSpeed { get; private set; }
        
        [field: SerializeField]
        public float DefaultSpeed { get; private set; }
        
        [field: SerializeField]
        public float SprintSpeed { get; private set; }
        
        [field: SerializeField]
        public float DashSpeed { get; private set; }
        
        // Speed Multipliers
        [field: SerializeField]
        public float SpeedMultiplier { get; private set; }
        
        [field: SerializeField]
        public float SlopeSpeedMultiplier { get; private set; }
        
        // Slope and Drag
        [field: SerializeField]
        public float GroundDrag { get; private set; }
        
        [field: SerializeField]
        public float SlopeDownwardForce { get; private set; }
        
        
        // Jump Variables
        [field: SerializeField]
        public float JumpForce { get; private set; }
        
        [field: SerializeField]
        public float JumpCooldown { get; private set; }
        
        [field: SerializeField]
        public float AirMultiplier { get; private set; }
        
        
        // Dash Variables
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
        public float DashFOV { get; private set; }
        
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