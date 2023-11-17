using Unite.EventSystem;
using Unite.Managers;
using UnityEngine;

namespace Unite.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerEvent onPlayerReady;
        
        private PlayerInputHandler inputHandler;
        private PlayerHealthHandler healthHandler;

        public PlayerInputHandler InputHandler => inputHandler;
        public PlayerHealthHandler HealthHandler => healthHandler;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            healthHandler = GetComponent<PlayerHealthHandler>();
            
            onPlayerReady.Raise(this);
        }
    }
}