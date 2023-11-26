using Unite.EventSystem;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerHealthHandler))]
    [RequireComponent(typeof(PlayerStatsHandler))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerData playerData;
        
        [SerializeField]
        private PlayerEvent onPlayerReady;
        
        private PlayerInputHandler inputHandler;
        private PlayerHealthHandler healthHandler;
        private PlayerStatsHandler statsHandler;

        public PlayerInputHandler InputHandler => inputHandler;
        public PlayerHealthHandler HealthHandler => healthHandler;
        public PlayerStatsHandler StatsHandler => statsHandler;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            healthHandler = GetComponent<PlayerHealthHandler>();
            statsHandler = GetComponent<PlayerStatsHandler>();
            
            playerData.SetupPlayer(this);
            
            onPlayerReady.Raise(this);
        }

        public void OnPlayerDead()
        {
            inputHandler.enabled = false;
            healthHandler.enabled = false;
        }
    }
}