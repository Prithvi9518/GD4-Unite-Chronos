using System.Collections;
using System.Collections.Generic;
using Unite.EventSystem;
using Unite.TimeStop;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Unite.Enemies.Spawning
{
    /// <summary>
    /// DEPRECATED - Enemy spawner used to spawn enemies endlessly.
    /// As of now, we are spawning enemies in waves within confined battle zones,
    /// using the EnemyWaveSpawner class.
    ///
    /// <seealso cref="EnemyWaveSpawner"/>
    /// </summary>
    public class EnemySpawner : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private List<EnemyData> enemyScriptableObjects;
        
        [Header("Spawn Distance Configuration")]
        [SerializeField]
        private float minSpawnDistanceFromPlayer;

        [SerializeField]
        private float maxSpawnDistanceFromPlayer;

        [Header("Enemy Numbers Configuration")]
        [SerializeField]
        private int enemiesSpawnedAtOnce;

        [SerializeField]
        private int maxEnemiesSpawned;

        [Header("Spawn Delay/Interval")]
        [SerializeField]
        private float spawnDelay;

        [Header("Spawner Ready Event")]
        [SerializeField]
        private EnemySpawnerEvent onSpawnerReady;

        private Transform player;
        
        private bool isSpawning;

        private int numEnemiesSpawned;

        private Dictionary<int, IObjectPool<Enemy>> enemyPoolDictionary = new();
        private IEnumerator spawnCoroutine;

        private void Awake()
        {
            SetupEnemyPools();
        }

        private void Start()
        {
            onSpawnerReady.Raise(this);
        }

        public void Initialize(Player.Player p)
        { 
            player = p.transform;
        }

        public void StartSpawning()
        {
            if (player == null) return;
            
            isSpawning = true;
            StartCoroutine(SpawnEnemiesAtInterval());
        }

        private IEnumerator SpawnEnemiesAtInterval()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);

                if (!isSpawning) continue;
                for (int i = 0; i < enemiesSpawnedAtOnce; i++)
                {
                    if(numEnemiesSpawned >= maxEnemiesSpawned) continue;
                    
                    SpawnRandomEnemy();
                    numEnemiesSpawned++;
                }
            }
        }

        private void SpawnRandomEnemy()
        {
            int randomIndex = Random.Range(0, enemyScriptableObjects.Count);

            Enemy enemy = GetAndSetupEnemy(randomIndex);

            Vector3 spawnPosition = GetRandomPositionAroundPlayer();

            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 2f, -1))
            {
                enemy.Agent.Warp(hit.position);
            }
        }

        private Enemy GetAndSetupEnemy(int index)
        {
            Enemy enemy = enemyPoolDictionary[index].Get();

            enemy.SetEnemyPool(enemyPoolDictionary[index]);
            enemyScriptableObjects[index].SetupEnemy(enemy, player);

            return enemy;
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
                    actionOnGet:OnGetEnemy,
                    actionOnDestroy:OnDestroyEnemy
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

        private void OnDestroyEnemy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            isSpawning = !isTimeStopped;
        }
    }
}