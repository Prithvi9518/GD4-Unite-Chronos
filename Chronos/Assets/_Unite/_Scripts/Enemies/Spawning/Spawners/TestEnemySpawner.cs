using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.Spawning
{
    public class TestEnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyData enemyData;

        [SerializeField]
        private string playerTag;

        private Transform playerTransform;
        
        private void Start()
        {
            playerTransform = GameObject.FindWithTag(playerTag).transform;
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            Enemy enemy = Instantiate(enemyData.EnemyPrefab, transform.position, Quaternion.identity);
            
            enemy.gameObject.SetActive(true);
            enemy.OnGetFromPool(playerTransform);
            enemyData.SetupEnemy(enemy, playerTransform);
            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 2f, -1))
            {
                enemy.Agent.Warp(hit.position);
            }
        }
    }
}