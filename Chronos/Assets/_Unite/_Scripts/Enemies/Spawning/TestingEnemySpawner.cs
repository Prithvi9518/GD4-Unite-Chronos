using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.Spawning
{
    public class TestingEnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private Transform[] spawnPositions;

        private Transform playerTransform;

        public void InitializePlayer(Player.Player player)
        {
            this.playerTransform = player.transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            foreach (Transform spawnTransform in spawnPositions)
            {
                Enemy enemy = Instantiate(enemyData.EnemyPrefab, spawnTransform.position, Quaternion.identity);
                enemy.gameObject.SetActive(true);
                enemy.OnGetFromPool(playerTransform);
                enemyData.SetupEnemy(enemy, playerTransform);
                
                NavMeshHit hit;
                if (NavMesh.SamplePosition(spawnTransform.position, out hit, 2f, -1))
                {
                    enemy.Agent.Warp(hit.position);
                }
            }
        }
    }
}