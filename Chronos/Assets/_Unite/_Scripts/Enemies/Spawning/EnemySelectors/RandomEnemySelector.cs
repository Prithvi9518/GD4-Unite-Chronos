using System.Collections.Generic;
using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class RandomEnemySelector : ISelectEnemy
    {
        public EnemySpawnConfig SelectEnemySpawn(List<EnemySpawnConfig> enemySpawns)
        {
            int enemyIndex = Random.Range(0, enemySpawns.Count);
            return enemySpawns[enemyIndex];
        }
    }
}