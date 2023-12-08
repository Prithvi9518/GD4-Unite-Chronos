using StarterAssets;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerHealthHandler))]
    [RequireComponent(typeof(PlayerStatsHandler))]
    [RequireComponent(typeof(PlayerGunHandler))]
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
        private FirstPersonController controller;

        public PlayerInputHandler InputHandler => inputHandler;
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
            
            controller = GetComponent<FirstPersonController>();
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