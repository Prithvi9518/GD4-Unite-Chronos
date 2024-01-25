using System.Collections.Generic;

namespace Unite.Enemies.Spawning
{
    public interface ISelectEnemy
    {
        public EnemySpawnConfig SelectEnemySpawn(List<EnemySpawnConfig> enemySpawns);
    }
}