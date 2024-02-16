using StarterAssets;
using Unite.AbilitySystem;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(PlayerHealthHandler))]
    [RequireComponent(typeof(PlayerStatsHandler))]
    [RequireComponent(typeof(PlayerGunHandler))]
    [RequireComponent(typeof(PlayerStatusEffectable))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerData playerData;
        
        [SerializeField]
        private PlayerEvent onPlayerReady;
        
        private PlayerHealthHandler healthHandler;
        private PlayerStatsHandler statsHandler;
        private PlayerGunHandler gunHandler;
        private IHandlePlayerMovement movementHandler;
        private FirstPersonController controller;
        private PlayerStatusEffectable statusEffectable;
        // Temporary workaround
        private Ability ability;

        public PlayerHealthHandler HealthHandler => healthHandler;
        public PlayerStatsHandler StatsHandler => statsHandler;
        public PlayerGunHandler GunHandler => gunHandler;
        public IHandlePlayerMovement MovementHandler => movementHandler;
        public PlayerStatusEffectable StatusEffectable => statusEffectable;

        public Ability Ability => ability;

        private void Awake()
        {
            healthHandler = GetComponent<PlayerHealthHandler>();
            
            statsHandler = GetComponent<PlayerStatsHandler>();
            gunHandler = GetComponent<PlayerGunHandler>();

            movementHandler = GetComponent<IHandlePlayerMovement>();
            
            controller = GetComponent<FirstPersonController>();

            statusEffectable = GetComponent<PlayerStatusEffectable>();

            ability = GetComponent<Ability>();
        }

        private void Start()
        {
            playerData.SetupPlayer(this);
            onPlayerReady.Raise(this);
        }

        public void OnPlayerDead()
        {
            healthHandler.enabled = false;
            gunHandler.enabled = false;
            movementHandler.ToggleMovement(false);
        }
    }
}