using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.Spawning
{
    public class EnemyWaveSpawner : MonoBehaviour
    {
        private int numEnemiesAlive;

        private ISelectEnemy enemySelector = new WeightedRandomEnemySelector();

        public int NumEnemiesAlive => numEnemiesAlive;
        
        public void SpawnEnemies(EnemyWave enemyWave, IProvideSpawnPosition spawnPositionProvider, Transform playerTransform)
        {
            numEnemiesAlive = 0;
            
            int availableCurrency = enemyWave.CurrencyToSpend;
            List<EnemySpawnConfig> validEnemiesToSpawn = new List<EnemySpawnConfig>(enemyWave.EnemySpawns);
            
            while (availableCurrency > 0 && validEnemiesToSpawn.Count > 0)
            {
                EnemySpawnConfig enemySpawn = enemySelector.SelectEnemySpawn(validEnemiesToSpawn);
                EnemyData enemyData = enemySpawn.EnemyData;
                int enemyCost = enemyData.Cost;

                if (availableCurrency - enemyCost >= 0)
                {
                    var spawnPos = spawnPositionProvider.GetSpawnPosition();
                    SpawnEnemy(enemyData, spawnPos, playerTransform);
                    
                    numEnemiesAlive++;
                    availableCurrency -= enemyCost;
                }
                else if (availableCurrency - enemyCost < 0)
                {
                    validEnemiesToSpawn.Remove(enemySpawn);
                }
                else if (availableCurrency <= 0)
                    break;
            }
        }
        
        private void SpawnEnemy(EnemyData enemyData, Vector3 spawnPosition, Transform playerTransform)
        {
            NavMeshHit hit;
            if (!NavMesh.SamplePosition(spawnPosition, out hit, 1000f, -1)) return;
            
            // Enemy enemy = Instantiate(enemyData.EnemyPrefab, hit.position, Quaternion.identity);
            // enemy.gameObject.SetActive(true);
            // enemy.OnGetFromPool(playerTransform);
            // enemyData.SetupEnemy(enemy, playerTransform);

            Enemy enemy = EnemyPoolManager.Instance.EnemyPools[enemyData].Get();
            enemy.SetEnemyPool(EnemyPoolManager.Instance.EnemyPools[enemyData]);
            enemy.transform.SetPositionAndRotation(hit.position, Quaternion.identity);
            enemyData.SetupEnemy(enemy, playerTransform);

            enemy.Agent.enabled = true;
            enemy.Agent.Warp(hit.position);
        }

        public void DecrementEnemiesAliveCount()
        {
            numEnemiesAlive--;
        }
    }
}