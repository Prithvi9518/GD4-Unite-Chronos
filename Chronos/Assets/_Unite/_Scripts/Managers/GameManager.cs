using Unite.Core.Game;
using Unite.Enemies.Spawning;
using Unite.EventSystem;
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
        private EnemySpawner enemySpawner;

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

        public void Initialize(Player.Player p, EnemySpawner es)
        {
            player = p;
            enemySpawner = es;
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
                default:
                    break;
            }
        }

        private void HandleGameStart()
        {
            if (currentState != GameState.Start) return;
            if (player == null || enemySpawner == null) return;
            
            enemySpawner.Initialize(player);
            enemySpawner.StartSpawning();
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