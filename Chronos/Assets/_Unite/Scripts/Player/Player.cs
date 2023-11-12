using UnityEngine;

namespace Unite
{
    public class Player : MonoBehaviour
    {
        private PlayerInputHandler inputHandler;
        private PlayerHealthHandler healthHandler;
        
        public PlayerInputHandler InputHandler => inputHandler;
        public PlayerHealthHandler HealthHandler => healthHandler;

        public delegate void HandlePlayerReady();
        public static event HandlePlayerReady OnPlayerReady;

        private void Awake()
        {
            inputHandler = GetComponent<PlayerInputHandler>();
            healthHandler = GetComponent<PlayerHealthHandler>();
            
            ReferenceManager.Instance.Player = this;
            OnPlayerReady?.Invoke();
        }
    }
}