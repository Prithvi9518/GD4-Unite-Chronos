using Unite.Core.Game;
using Unite.Enemies.Spawning;
using UnityEngine;

namespace Unite.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
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
    }
}