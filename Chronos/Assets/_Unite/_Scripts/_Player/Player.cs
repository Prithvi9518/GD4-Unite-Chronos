using StarterAssets;
using Unite.EventSystem;
using Unite.StatusEffectSystem;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerHealthHandler))]
    [RequireComponent(typeof(PlayerStatsHandler))]
    [RequireComponent(typeof(PlayerGunHandler))]
    [RequireComponent(typeof(PlayerMovementHandler))]
    [RequireComponent(typeof(PlayerStatusEffectable))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerData playerData;
        
        [SerializeField]
        private PlayerEvent onPlayerReady;
        
        private PlayerInputHandler inputHandler;
        private PlayerHealthHandler healthHandler;
        private PlayerStatsHandler statsHandler;
        private PlayerGunHandler gunHandler;
        private PlayerMovementHandler movementHandler;
        private FirstPersonController controller;
        private PlayerStatusEffectable statusEffectable;

        public PlayerHealthHandler HealthHandler => healthHandler;
        public PlayerStatsHandler StatsHandler => statsHandler;
        public PlayerGunHandler GunHandler => gunHandler;
        public PlayerInputHandler InputHandler => inputHandler;
        public PlayerMovementHandler MovementHandler => movementHandler;
        public PlayerStatusEffectable StatusEffectable => statusEffectable;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            
            healthHandler = GetComponent<PlayerHealthHandler>();
            
            statsHandler = GetComponent<PlayerStatsHandler>();
            gunHandler = GetComponent<PlayerGunHandler>();

            movementHandler = GetComponent<PlayerMovementHandler>();
            
            controller = GetComponent<FirstPersonController>();

            statusEffectable = GetComponent<PlayerStatusEffectable>();
        }

        private void Start()
        {
            playerData.SetupPlayer(this);
            onPlayerReady.Raise(this);
        }

        public void OnPlayerDead()
        {
            inputHandler.enabled = false;
            healthHandler.enabled = false;
            gunHandler.enabled = false;
            controller.enabled = false;
        }
    }
}