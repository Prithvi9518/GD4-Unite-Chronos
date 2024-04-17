using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.Spawning
{
    /// <summary>
    /// Spawns enemies in waves, after selecting them using a weighted-random
    /// selection strategy.
    ///
    /// <seealso cref="EnemyWave"/>
    /// </summary>
    public class EnemyWaveSpawner : MonoBehaviour
    {
        private int numEnemiesAlive;

        private ISelectEnemy enemySelector = new WeightedRandomEnemySelector();

        public int NumEnemiesAlive => numEnemiesAlive;
        
        /// <summary>
        /// Spawns enemies using a currency-based system.
        /// Each enemy wave has a set amount of currency to spend, and each enemy type has an associated cost.
        /// The spawner can spend currency to spawn enemies until it cannot afford any more enemies.
        /// </summary>
        /// 
        /// <param name="enemyWave">Holds information about the enemy wave to spawn</param>
        /// <param name="spawnPositionProvider">Provides the enemy's spawn position.</param>
        /// <param name="playerTransform">Player's transform, which is set as the enemy's target after spawning.</param>
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
            
            Enemy enemy = EnemyPoolManager.Instance.EnemyPools[enemyData].Get();
            enemy.SetEnemyPool(EnemyPoolManager.Instance.EnemyPools[enemyData]);
            enemy.transform.SetPositionAndRotation(hit.position, Quaternion.identity);
            enemyData.SetupEnemy(enemy, playerTransform);

            enemy.Agent.Warp(hit.position);
        }

        public void DecrementEnemiesAliveCount()
        {
            numEnemiesAlive--;
        }
    }
}