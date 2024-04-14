using System.Collections.Generic;

namespace Unite.Enemies.Spawning
{
    /// <summary>
    /// Any script that handles selection of enemies to spawn must implement this interface.
    ///
    /// <seealso cref="WeightedRandomEnemySelector"/>
    /// <seealso cref="RandomEnemySelector"/>
    /// </summary>
    public interface ISelectEnemy
    {
        public EnemySpawnConfig SelectEnemySpawn(List<EnemySpawnConfig> enemySpawns);
    }
}