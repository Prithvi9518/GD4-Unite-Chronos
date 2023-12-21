using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.Spawning
{
    public class EnemyWaveSpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyData enemyData;

        public void SpawnEnemy(Vector3 spawnPosition, Transform playerTransform)
        {
            Enemy enemy = Instantiate(enemyData.EnemyPrefab, spawnPosition, Quaternion.identity);
            enemy.gameObject.SetActive(true);
            enemy.OnGetFromPool(playerTransform);
            enemyData.SetupEnemy(enemy, playerTransform);
            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 2f, -1))
            {
                enemy.Agent.Warp(hit.position);
            }
        }
    }
}