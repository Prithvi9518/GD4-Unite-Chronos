using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Unite.Enemies.Spawning
{
    public class EnemyWaveSpawner : MonoBehaviour
    {
        [SerializeField]
        private EnemyData[] enemies;

        [SerializeField]
        private int currencyToSpend;

        private int remainingCurrency;

        private void Start()
        {
            remainingCurrency = currencyToSpend;
        }

        public void SpawnEnemies(Vector3 spawnPosition, Transform playerTransform)
        {
            List<EnemyData> validEnemiesToSpawn = new List<EnemyData>(enemies);
            while (remainingCurrency > 0 && validEnemiesToSpawn.Count > 0)
            {
                int enemyIndex = Random.Range(0, validEnemiesToSpawn.Count);
                EnemyData enemyData = validEnemiesToSpawn[enemyIndex];
                int enemyCost = enemyData.Cost;

                if (remainingCurrency - enemyCost >= 0)
                {
                    SpawnEnemy(enemyData, spawnPosition, playerTransform);
                    remainingCurrency -= enemyCost;
                }
                else if (remainingCurrency - enemyCost < 0)
                {
                    validEnemiesToSpawn.Remove(enemyData);
                }
                else if (remainingCurrency <= 0)
                    break;
            }
        }
        
        private void SpawnEnemy(EnemyData enemyData, Vector3 spawnPosition, Transform playerTransform)
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