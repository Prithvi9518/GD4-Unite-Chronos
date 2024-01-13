using StarterAssets;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerHealthHandler))]
    [RequireComponent(typeof(PlayerStatsHandler))]
    [RequireComponent(typeof(PlayerGunHandler))]
    [RequireComponent(typeof(PlayerMovementHandler))]
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

        public PlayerHealthHandler HealthHandler => healthHandler;
        public PlayerStatsHandler StatsHandler => statsHandler;
        public PlayerGunHandler GunHandler => gunHandler;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            
            healthHandler = GetComponent<PlayerHealthHandler>();
            
            statsHandler = GetComponent<PlayerStatsHandler>();
            
            gunHandler = GetComponent<PlayerGunHandler>();
            gunHandler.SetInputHandler(inputHandler);

            movementHandler = GetComponent<PlayerMovementHandler>();
            
            controller = GetComponent<FirstPersonController>();
        }

        private void Start()
        {
            playerData.SetupPlayer(this);
            movementHandler.UpdateSpeedValue();
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