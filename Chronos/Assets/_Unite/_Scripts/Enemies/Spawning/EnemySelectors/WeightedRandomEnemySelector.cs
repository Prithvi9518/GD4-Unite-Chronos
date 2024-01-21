using System.Collections.Generic;
using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class WeightedRandomEnemySelector : ISelectEnemy
    {
        private List<EnemySpawnConfig> enemySpawnConfigs;
        private List<float> weights = new();
        
        public EnemySpawnConfig SelectEnemySpawn(List<EnemySpawnConfig> enemySpawns)
        {
            UpdateWeights(enemySpawns);
            
            float value = Random.value;
            
            for (int i = 0; i < weights.Count; i++)
            {
                if (value < weights[i])
                {
                    EnemySpawnConfig enemySpawn = enemySpawns[i];
                    return enemySpawn;
                }
                
                value -= weights[i];
            }

            return enemySpawns[0];
        }
        
        private void UpdateWeights(List<EnemySpawnConfig> enemySpawns)
        {
            weights.Clear();
            float totalWeight = 0;
            
            for (int i = 0; i < enemySpawns.Count; i++)
            {
                float weight = enemySpawns[i].GetWeight();
                weights.Add(weight);
                totalWeight += weight;
            }

            for (int i = 0; i < weights.Count; i++)
            {
                weights[i] /= totalWeight;
            }
        }
    }
}