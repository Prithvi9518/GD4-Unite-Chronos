using System.Collections;
using System.Collections.Generic;
using Unite.TimeStop;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Unite.Enemies.Spawning
{
    public class EnemySpawner : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private List<EnemyData> enemyScriptableObjects;

        [SerializeField]
        private Transform player;
        
        [SerializeField]
        private float minSpawnDistanceFromPlayer;

        [SerializeField]
        private float maxSpawnDistanceFromPlayer;

        [SerializeField]
        private EnemySpawnMode spawnMode;

        [Header("Individual Spawn Mode Settings")]
        [SerializeField]
        private InputActionReference individualSpawnModeAction;

        [SerializeField]
        private float individualSpawnDistance;

        [Header("Demo Spawn Mode Settings")] 
        [SerializeField] 
        private EnemyData demoBossEnemyData;

        [SerializeField]
        private float bossSpawnDistance;

        [SerializeField]
        private int numSpawnedInDemoWave;

        [SerializeField]
        private InputActionReference spawnDemoWaveAction;

        [SerializeField] 
        private InputActionReference spawnDemoBossAction;

        [Header("Interval Spawn Mode Settings")]
        [SerializeField]
        private int enemiesSpawnedAtOnce;

        [SerializeField]
        private float spawnDelay;

        private Dictionary<int, IObjectPool<Enemy>> enemyPoolDictionary = new();
        private IEnumerator spawnCoroutine;

        private bool isSpawning;

        private void Awake()
        {
            SetupEnemyPools();
        }

        private void Start()
        {
            if (player == null)
            {
                player = ReferenceManager.Player.transform;
            }
            
            if (player == null) return; // in case the ReferenceManager does not have a reference to the player yet
            StartSpawning();
        }

        private void OnEnable()
        {
            individualSpawnModeAction.action.performed += DoIndividualSpawning;
            individualSpawnModeAction.action.Enable();

            spawnDemoWaveAction.action.performed += SpawnDemoWave;
            spawnDemoWaveAction.action.Enable();

            spawnDemoBossAction.action.performed += SpawnDemoBoss;
            spawnDemoBossAction.action.Enable();
        }

        private void OnDisable()
        {
            if (TimeStopManager.Instance == null) return;

            individualSpawnModeAction.action.performed -= DoIndividualSpawning;
            individualSpawnModeAction.action.Disable();
            
            spawnDemoWaveAction.action.performed -= SpawnDemoWave;
            spawnDemoWaveAction.action.Disable();
            
            spawnDemoBossAction.action.performed -= SpawnDemoBoss;
            spawnDemoBossAction.action.Disable();
        }

        private void StartSpawning()
        {
            if (spawnMode != EnemySpawnMode.Interval) return;
            
            isSpawning = true;
            StartCoroutine(SpawnEnemiesAtInterval());
        }

        private void DoIndividualSpawning(InputAction.CallbackContext ctx)
        {
            if (spawnMode != EnemySpawnMode.Individual) return;

            Vector3 spawnPos = player.position + player.forward * individualSpawnDistance;
            int randomIndex = Random.Range(0, enemyScriptableObjects.Count);

            Enemy enemy = GetAndSetupEnemy(randomIndex);

            if (!NavMesh.SamplePosition(spawnPos, out var hit, 2f, -1)) return;
            enemy.Agent.Warp(hit.position);
            enemy.transform.LookAt(player.position);
        }

        private void SpawnDemoWave(InputAction.CallbackContext ctx)
        {
            if (spawnMode != EnemySpawnMode.Demo) return;
            
            for (int i = 0; i < numSpawnedInDemoWave; i++)
            {
                SpawnRandomEnemy();
            }
        }

        private void SpawnDemoBoss(InputAction.CallbackContext ctx)
        {
            if (spawnMode != EnemySpawnMode.Demo) return;
            
            Vector3 spawnPos = player.position + player.forward * bossSpawnDistance;
            
            Enemy enemy = Instantiate(demoBossEnemyData.EnemyPrefab);
            demoBossEnemyData.SetupEnemy(enemy, player);
            
            
            if (!NavMesh.SamplePosition(spawnPos, out var hit, 2f, -1)) return;
            enemy.Agent.Warp(hit.position);
            enemy.transform.LookAt(player.position);
        }

        private IEnumerator SpawnEnemiesAtInterval()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);

                if (!isSpawning) continue;
                for (int i = 0; i < enemiesSpawnedAtOnce; i++)
                {
                    SpawnRandomEnemy();
                }
            }
        }

        private void SpawnRandomEnemy()
        {
            int randomIndex = Random.Range(0, enemyScriptableObjects.Count);

            Enemy enemy = GetAndSetupEnemy(randomIndex);

            Vector3 spawnPosition = new Vector3();

            switch (spawnMode)
            {
                case EnemySpawnMode.Interval:
                    spawnPosition = GetRandomPositionAroundPlayer();
                    break;
                case EnemySpawnMode.Demo:
                    spawnPosition = GetRandomPositionInFrontOfPlayer();
                    break;
                default:
                    break;
            }

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
        
        private Vector3 GetRandomPositionInFrontOfPlayer()
        {
            float randomDistance = Random.Range(minSpawnDistanceFromPlayer, maxSpawnDistanceFromPlayer);

            Quaternion randomAngle = Quaternion.Euler(0, Random.Range(-90, 91), 0);
            Vector3 direction = randomAngle * player.forward;

            Vector3 randomPosition = player.position + (direction * randomDistance);
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