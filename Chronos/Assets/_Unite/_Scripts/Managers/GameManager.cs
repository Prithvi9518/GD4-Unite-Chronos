using System;
using Unite.Bootstrap;
using Unite.Core.Game;
using Unite.EventSystem;
using Unite.Player;
using Unite.WeaponSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unite.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game State Events")]
        [SerializeField] 
        private GameEvent onGameSetup;
        
        [SerializeField] 
        private GameEvent onGameStart;
        
        [SerializeField] 
        private GameEvent onGameWin;
        
        [SerializeField] 
        private GameEvent onGameLose;

        [Header("Events related to loading levels")]
        [SerializeField]
        private GameEvent onStartSwitchToNextLevel;
        
        [SerializeField]
        private GameEvent onFinishSwitchToNextLevel;

        private GameState currentState;

        private Player.Player player;
        private Camera playerCamera;
        private WeaponHolder playerWeaponsHolder;

        private PlayerSpawnOnSceneLoad playerSpawn;

        public Action<float> OnProgressLevelLoad;

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
            
            TryStartGameForTestScenes();
        }

        public void InitializeCamera(Camera cam, Transform weaponsHolder)
        {
            Debug.Log("Initializing player camera.");
            playerCamera = cam;
            playerWeaponsHolder = weaponsHolder.GetComponent<WeaponHolder>();
            
            TryStartGameForTestScenes();
        }

        public void RegisterPlayerSpawn(PlayerSpawnOnSceneLoad spawn)
        {
            playerSpawn = spawn;
        }

        public void OnStartLoadingLevel(int currentLevel)
        {
            Debug.Log($"GameManager.{nameof(OnStartLoadingLevel)}");
            
            if(player != null)
                player.MovementHandler.DisableMovement();
            
            onStartSwitchToNextLevel.Raise();
        }

        public void OnProgressLoadingLevel(float progress)
        {
            OnProgressLevelLoad?.Invoke(progress);
        }

        public void OnFinishedLoadingLevel(int currentLevel)
        {
            Debug.Log($"GameManager.{nameof(OnFinishedLoadingLevel)}");
            
            if (currentLevel == 0)
            {
                SetupAndStartGame();
            }
            else
            {
                player.MovementHandler.EnableMovement();
                playerSpawn.SpawnPlayer(player);
            }
            
            onFinishSwitchToNextLevel.Raise();
        }

        public void SetupAndStartGame()
        {
            Debug.Log($"GameManager.{nameof(SetupAndStartGame)} called.");
            
            if (player == null)
            {
                Debug.LogError("GameManager.SetupAndStartGame() - no player found.");
                return;
            }

            if (playerCamera == null)
            {
                Debug.LogError("GameManager.SetupAndStartGame() - player camera not found.");
                return;
            }
            
            onGameSetup.Raise();
            SetGameState(GameState.Start);
        }

        private void TryStartGameForTestScenes()
        {
            Debug.Log($"GameManager.{nameof(TryStartGameForTestScenes)} called.");
            
            if (player == null || playerCamera == null) return;
            
            // Check if there is a bootloader present. If no bootloader, just start the game (to support test scenes).
            if (Bootloader.Instance != null) return;
            
            Debug.Log("No bootloader found after initializing player and camera. Setting up and starting game.");
            SetupAndStartGame();
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

        public void SwitchToNextLevel()
        {
            Bootloader.Instance.LoadNextLevel();
        }
    }
}