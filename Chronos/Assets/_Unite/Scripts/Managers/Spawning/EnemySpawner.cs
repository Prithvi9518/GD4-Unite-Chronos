using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

namespace Unite
{
    public class EnemySpawner : MonoBehaviour, ITimeStopSubscriber
    {
        private enum EnemySpawnMode
        {
            Debug,
            Demo,
            Interval
        }

        [SerializeField]
        private List<EnemyData> enemyScriptableObjects;

        [SerializeField]
        private Transform player;

        [SerializeField]
        private EnemySpawnMode spawnMode;

        [Header("Debug Spawn Mode Settings")]
        [SerializeField]
        private InputActionReference debugSpawnModeAction;

        [SerializeField]
        private float debugSpawnDistance;

        [Header("Interval Spawn Mode Settings")]
        [SerializeField]
        private int enemiesSpawnedAtOnce;

        [SerializeField]
        private float minSpawnDistanceFromPlayer;

        [SerializeField]
        private float maxSpawnDistanceFromPlayer;

        [SerializeField]
        private float spawnDelay;

        private Dictionary<int, IObjectPool<Enemy>> enemyPoolDictionary = new();
        private IEnumerator spawnCoroutine;

        private void Awake()
        {
            SetupEnemyPools();
        }

        private void Start()
        {
            if (spawnMode != EnemySpawnMode.Interval) return;
            spawnCoroutine = SpawnEnemiesAtInterval();
            StartCoroutine(spawnCoroutine);
        }

        private void OnEnable()
        {
            TimeStopManager.Instance.OnToggleTimeStop += HandleTimeStopEvent;

            debugSpawnModeAction.action.performed += DoDebugSpawning;
            debugSpawnModeAction.action.Enable();
        }

        private void OnDisable()
        {
            if (TimeStopManager.Instance == null) return;
            TimeStopManager.Instance.OnToggleTimeStop -= HandleTimeStopEvent;

            debugSpawnModeAction.action.performed -= DoDebugSpawning;
            debugSpawnModeAction.action.Disable();
        }

        private void DoDebugSpawning(InputAction.CallbackContext ctx)
        {
            if (spawnMode != EnemySpawnMode.Demo) return;

            Vector3 spawnPos = player.position + player.forward * debugSpawnDistance;
            int randomIndex = Random.Range(0, enemyScriptableObjects.Count);

            Enemy enemy = GetAndSetupEnemy(randomIndex);

            if (!NavMesh.SamplePosition(spawnPos, out var hit, 2f, -1)) return;
            enemy.Agent.Warp(hit.position);
            enemy.transform.LookAt(player.position);
        }

        private IEnumerator SpawnEnemiesAtInterval()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
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
            enemyScriptableObjects[index].SetupEnemy(enemy);
            enemy.DetectionHandler.Target = player;

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
            if (spawnCoroutine == null) return;

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