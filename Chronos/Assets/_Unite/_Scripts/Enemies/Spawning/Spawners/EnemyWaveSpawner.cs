using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Unite.Enemies.Spawning
{
    public class EnemyWaveSpawner : MonoBehaviour
    {
        private int numEnemiesAlive;

        public int NumEnemiesAlive => numEnemiesAlive;
        
        public void SpawnEnemies(EnemyWave enemyWave, IProvideSpawnPosition spawnPositionProvider, Transform playerTransform)
        {
            numEnemiesAlive = 0;
            
            int availableCurrency = enemyWave.CurrencyToSpend;
            List<EnemyData> validEnemiesToSpawn = new List<EnemyData>(enemyWave.Enemies);
            
            while (availableCurrency > 0 && validEnemiesToSpawn.Count > 0)
            {
                int enemyIndex = Random.Range(0, validEnemiesToSpawn.Count);
                EnemyData enemyData = validEnemiesToSpawn[enemyIndex];
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
                    validEnemiesToSpawn.Remove(enemyData);
                }
                else if (availableCurrency <= 0)
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

        public void DecrementEnemiesAliveCount()
        {
            numEnemiesAlive--;
        }
    }
}