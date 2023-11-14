using UnityEngine;

namespace Unite.Core.Player
{
    public class Player : MonoBehaviour
    {
        private PlayerInputHandler inputHandler;
        private PlayerHealthHandler healthHandler;

        public PlayerInputHandler InputHandler => inputHandler;
        public PlayerHealthHandler HealthHandler => healthHandler;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            healthHandler = GetComponent<PlayerHealthHandler>();
            
            ReferenceManager.Player = this;
        }
    }
}