using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Unite
{
    public class EnemySpawner : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private List<EnemyData> enemyScriptableObjects;

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

        private Dictionary<int, IObjectPool<Enemy>> enemyPoolDictionary = new();

        private int numCurrentlySpawned;

        private Vector3 spawnPosition;

        private IEnumerator spawnCoroutine;

        private void Awake()
        {
            SetupEnemyPools();
        }

        private void Start()
        {
            spawnCoroutine = SpawnEnemiesCoroutine();
            StartCoroutine(spawnCoroutine);
        }

        private void OnEnable()
        {
            TimeStopManager.Instance.ToggleTimeStop += HandleTimeStopEvent;
        }

        private void OnDisable()
        {
            if (TimeStopManager.Instance == null) return;
            TimeStopManager.Instance.ToggleTimeStop -= HandleTimeStopEvent;
        }

        private IEnumerator SpawnEnemiesCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                if (numCurrentlySpawned < maxSpawnedAtATime)
                {
                    SpawnRandomEnemy();
                    numCurrentlySpawned++;
                }
            }
        }

        private void SpawnRandomEnemy()
        {
            int randomIndex = Random.Range(0, enemyScriptableObjects.Count);

            Enemy enemy = enemyPoolDictionary[randomIndex].Get();

            enemy.SetEnemyPool(enemyPoolDictionary[randomIndex]);
            enemyScriptableObjects[randomIndex].SetupEnemy(enemy);
            enemy.DetectionHandler.Target = player;

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

        private void SetupEnemyPools()
        {
            for (int i = 0; i < enemyScriptableObjects.Count; i++)
            {
                IObjectPool<Enemy> enemyPool = new ObjectPool<Enemy>(
                    () => CreateEnemy(i - 1),
                    OnGetEnemy,
                    OnReleaseEnemy,
                    OnDestroyEnemy
                );
                enemyPoolDictionary.Add(i, enemyPool);
            }
        }

        private Enemy CreateEnemy(int index)
        {
            Enemy enemy = Instantiate(enemyScriptableObjects[index].EnemyPrefab);
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

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            if (isTimeStopped)
            {
                StopCoroutine(spawnCoroutine);
            }
            else
            {
                StartCoroutine(spawnCoroutine);
            }
        }
    }
}