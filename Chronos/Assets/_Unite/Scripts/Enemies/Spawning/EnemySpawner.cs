using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Unite
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Enemy> enemyPrefabs;

        [SerializeField]
        private Transform player;

        [SerializeField]
        private int maxSpawnedAtATime;

        [SerializeField]
        private float minSpawnDistanceFromPlayer;

        [SerializeField]
        private float maxSpawnDistanceFromPlayer;

        [SerializeField]
        private float spawnDelay;

        private IObjectPool<Enemy> enemyPool;

        private int numCurrentlySpawned;
        private float timeWhenLastSpawned;

        private Vector3 spawnPosition;

        private void Awake()
        {
            enemyPool = new ObjectPool<Enemy>(() => CreateEnemy(0), OnGetEnemy, OnReleaseEnemy, OnDestroyEnemy);
        }

        private void Update()
        {
            SpawnEnemies();
        }

        private void SpawnEnemies()
        {
            if(!CanSpawn()) return;

            SpawnRandomEnemy(Random.Range(0, enemyPrefabs.Count));

            timeWhenLastSpawned = Time.time;
            numCurrentlySpawned++;
        }

        private bool CanSpawn()
        {
            return numCurrentlySpawned < maxSpawnedAtATime
                && timeWhenLastSpawned + spawnDelay < Time.time;
        }

        private void SpawnRandomEnemy(int spawnIndex)
        {
            Enemy enemy = enemyPool.Get();
            enemy.SetEnemyPool(enemyPool);

            spawnPosition = GetRandomPositionAroundPlayer();

            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 2f, -1))
            {
                enemy.Agent.Warp(hit.position);
            }
        }

        private Vector3 GetRandomPositionAroundPlayer()
        {
            float randomDistance = Random.Range(minSpawnDistanceFromPlayer, maxSpawnDistanceFromPlayer);

            Vector2 randomPointInCircle = Random.insideUnitCircle.normalized * randomDistance;
            Vector3 randomPosition = player.position + new Vector3(randomPointInCircle.x, 0, randomPointInCircle.y);

            return randomPosition;
        }

        private Enemy CreateEnemy(int prefabIndex)
        {
            Enemy enemy = Instantiate(enemyPrefabs[prefabIndex]);
            return enemy;
        }

        private void OnGetEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
            enemy.OnGetFromPool(player.transform);
        }

        private void OnReleaseEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            numCurrentlySpawned--;
        }

        private void OnDestroyEnemy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(player.transform.position, spawnPosition);
        }
    }
}