using System;
using Unite.ActionSystem;
using Unite.Bootstrap;
using Unite.Core.Game;
using Unite.Core.Input;
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
        private GameEvent onGameLose;

        [Header("Events related to loading levels")]
        [SerializeField]
        private GameEvent onStartSwitchToNextLevel;
        
        [SerializeField]
        private GameEvent onFinishSwitchToNextLevel;

        [Header("Event for test scenes so that gun and hud get initialized")]
        [SerializeField]
        private GameEvent onStartTestScene;

        private GameState currentState;

        private Player.Player player;
        private Camera playerCamera;
        private WeaponHolder playerWeaponsHolder;

        private PlayerSpawn playerSpawn;

        private GameLevel currentLevel;

        public Action<float> OnProgressLevelLoad;
        public Action<GameLevel> OnStartLevel_UpdateTimeTracking;
        public Action<GameLevel> OnFinishLevel_UpdateTimeTracking;

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

        public void RegisterPlayerSpawn(PlayerSpawn spawn)
        {
            playerSpawn = spawn;
            TryStartGameForTestScenes();
        }

        public void OnStartLoadingLevel()
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

        public void OnFinishedLoadingLevel(int levelIndex, GameLevel level)
        {
            Debug.Log($"GameManager.{nameof(OnFinishedLoadingLevel)}");
            
            if (levelIndex == 0)
            {
                SetupAndStartGame();
            }
            else
            {
                playerSpawn.SpawnPlayer(player);
                InputManager.Instance.EnableDefaultActions();
                CursorLockHandler.Instance.HideAndLockCursor();
                player.MovementHandler.EnableMovement();
            }
            
            onFinishSwitchToNextLevel.Raise();
            
            if(level.OnLoadLevel != null)
                level.OnLoadLevel.Raise();

            foreach (var action in level.ActionsOnLoad)
            {
                ActionExecutionManager.Instance.ExecuteAction(action);
            }
            
            currentLevel = level;
            OnStartLevel_UpdateTimeTracking?.Invoke(currentLevel);
        }

        public void SetupAndStartGame()
        {
            Debug.Log($"GameManager.{nameof(SetupAndStartGame)} called.");
            
            if (player == null)
            {
                if (playerSpawn != null)
                {
                    player = playerSpawn.InstantiateAndSpawnPlayer();
                }

                if (player == null)
                {
                    Debug.LogError("GameManager.SetupAndStartGame() - no player found.");
                    return;
                }
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

            if (player == null)
            {
                if (playerSpawn == null) return;

                player = playerSpawn.InstantiateAndSpawnPlayer();
            }
            
            if (player == null || playerCamera == null) return;
            
            // Check if there is a bootloader present. If no bootloader, just start the game (to support test scenes).
            if (Bootloader.Instance != null) return;
            
            Debug.Log("No bootloader found after initializing player and camera. Setting up and starting game.");
            
            onStartTestScene.Raise();
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
                    HandleGameStartState();
                    break;
                case GameState.PlayerDead:
                    HandlePlayerDeadState();
                    break;
            }
        }

        private void HandleGameStartState()
        {
            if (currentState != GameState.Start) return;
            if (player == null) return;
            
            Debug.Log("GAME START");
            CursorLockHandler.Instance.HideAndLockCursor();
            onGameStart.Raise();
        }

        private void HandlePlayerDeadState()
        {
            player.MovementHandler.DisableMovement();
            InputManager.Instance.DisableDefaultActions();
            onGameLose.Raise();
            Debug.Log("LOSE");
        }

        public void HandleRestart()
        {
            Debug.Log("RESTART");
            Bootloader.Instance.ReloadCurrentLevel();
            SetGameState(GameState.Start);
        }

        public void HandleLose()
        {
            if (currentState != GameState.Start) return;
            SetGameState(GameState.PlayerDead);
        }

        public void SwitchToNextLevel()
        {
            OnFinishLevel_UpdateTimeTracking?.Invoke(currentLevel);
            Bootloader.Instance.LoadNextLevel();
        }
    }
}