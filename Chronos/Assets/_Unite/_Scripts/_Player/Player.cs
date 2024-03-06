using Unite.AbilitySystem;
using Unite.EventSystem;
using Unite.Managers;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(PlayerHealthHandler))]
    [RequireComponent(typeof(PlayerGunHandler))]
    [RequireComponent(typeof(PlayerStatusEffectable))]
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private Transform orientation;

        [SerializeField]
        private Transform cameraRoot;
        
        [SerializeField]
        private PlayerData playerData;
        
        [SerializeField]
        private PlayerEvent onPlayerReady;
        
        private PlayerHealthHandler healthHandler;
        private PlayerStatsHandler statsHandler;
        private PlayerGunHandler gunHandler;
        private IHandlePlayerMovement movementHandler;
        private PlayerStatusEffectable statusEffectable;
        
        // Temporary workaround
        private Ability ability;

        public PlayerHealthHandler HealthHandler => healthHandler;
        public PlayerStatsHandler StatsHandler => statsHandler;
        public PlayerGunHandler GunHandler => gunHandler;
        public IHandlePlayerMovement MovementHandler => movementHandler;
        public PlayerStatusEffectable StatusEffectable => statusEffectable;
        public Transform Orientation => (orientation != null) ? orientation : transform;
        public Transform CameraRoot => (cameraRoot != null) ? cameraRoot : transform;

        public Ability Ability => ability;

        private void Awake()
        {
            healthHandler = GetComponent<PlayerHealthHandler>();
            
            statsHandler = new PlayerStatsHandler();
            gunHandler = GetComponent<PlayerGunHandler>();

            movementHandler = GetComponent<IHandlePlayerMovement>();
            
            statusEffectable = GetComponent<PlayerStatusEffectable>();

            ability = GetComponent<Ability>();
            
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            playerData.SetupPlayer(this);
            onPlayerReady.Raise(this);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameLose += OnGameOver;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameLose -= OnGameOver;
        }

        public void OnPlayerDead()
        {
            healthHandler.enabled = false;
            gunHandler.enabled = false;
            movementHandler.DisableMovement();
        }

        private void OnGameOver()
        {
            Debug.Log("Player.OnGameOver");
            playerData.SetupPlayer(this);
        }
    }
}