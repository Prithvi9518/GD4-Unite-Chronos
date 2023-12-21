using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Unite.Enemies.Spawning
{
    public class IndividualEnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyData enemyScriptableObject;

        [SerializeField] 
        private float spawnDistanceFromPlayer;

        [SerializeField] 
        private InputActionReference spawnAction;

        [SerializeField] 
        private string playerTag;

        private Transform player;

        private void OnEnable()
        {
            spawnAction.action.performed += SpawnEnemy;
        }

        private void OnDisable()
        {
            spawnAction.action.performed -= SpawnEnemy;
        }

        private void Start()
        {
            player = GameObject.FindWithTag(playerTag).transform;
        }

        private void SpawnEnemy(InputAction.CallbackContext ctx)
        {
            Vector3 spawnPos = player.forward * spawnDistanceFromPlayer;

            Enemy enemy = Instantiate(enemyScriptableObject.EnemyPrefab, spawnPos, Quaternion.identity);
            enemyScriptableObject.SetupEnemy(enemy, player);
            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPos, out hit, 2f, -1))
            {
                enemy.Agent.Warp(hit.position);
            }
        }
    }
}