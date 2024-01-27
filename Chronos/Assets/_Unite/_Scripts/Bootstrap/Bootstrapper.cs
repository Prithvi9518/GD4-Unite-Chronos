using Unite.Core.Game;
using Unite.Enemies.Spawning;
using Unite.Managers;
using UnityEngine;

namespace Unite.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        private Player.Player player;
        private EnemySpawner enemySpawner;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void HandlePlayerReadyEvent(Player.Player p)
        {
            player = p;
            CheckAndDoBootstrap();
        }

        public void HandleEnemySpawnerReadyEvent(EnemySpawner spawner)
        {
            enemySpawner = spawner;
            CheckAndDoBootstrap();
        }

        private void CheckAndDoBootstrap()
        {
            if (player == null || enemySpawner == null) return;
            Managers.GameManager.Instance.Initialize(player, enemySpawner);
            Managers.GameManager.Instance.SetGameState(GameState.Start);
        }
    }
}