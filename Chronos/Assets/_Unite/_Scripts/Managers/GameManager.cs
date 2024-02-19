using Unite.Bootstrap;
using Unite.Core.Game;
using Unite.EventSystem;
using Unite.WeaponSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unite.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        [SerializeField] 
        private GameEvent onGameStart;
        
        [SerializeField] 
        private GameEvent onGameWin;
        
        [SerializeField] 
        private GameEvent onGameLose;
        
        private GameState currentState;

        private Player.Player player;
        private Camera playerCamera;
        private WeaponHolder playerWeaponsHolder;

        public Player.Player Player => player;
        public Camera PlayerCamera => playerCamera;
        public WeaponHolder WeaponsHolder => playerWeaponsHolder;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
            
            currentState = GameState.Bootstrap;
        }

        public void InitializePlayer(Player.Player p)
        {
            Debug.Log("Initializing player");
            player = p;
            
            TryStartGame();
        }

        public void InitializeCamera(Camera cam, Transform weaponsHolder)
        {
            Debug.Log("Initializing player camera.");
            playerCamera = cam;
            playerWeaponsHolder = weaponsHolder.GetComponent<WeaponHolder>();
            
            TryStartGame();
        }

        private void TryStartGame()
        {
            if (player == null || playerCamera == null) return;
            
            // Check if there is a bootloader present. If no bootloader, just start the game (to support test scenes).
            if (Bootloader.Instance != null) return;
            
            Debug.Log("No bootloader found after initializing player and camera. Setting GameState = GameState.Start");
            SetGameState(GameState.Start);
        }

        public void SetGameState(GameState newState)
        {
            currentState = newState;
            switch (currentState)
            {
                case GameState.Bootstrap:
                    break;
                case GameState.Start:
                    HandleGameStart();
                    break;
                case GameState.PlayerDead:
                    break;
            }
        }

        private void HandleGameStart()
        {
            if (currentState != GameState.Start) return;
            if (player == null) return;
            
            Debug.Log("GAME START");
            onGameStart.Raise();
        }

        private void HandleRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SetGameState(GameState.Start);
        }

        public void HandleLose()
        {
            SetGameState(GameState.PlayerDead);
            Debug.Log("LOSE");
        }
    }
}