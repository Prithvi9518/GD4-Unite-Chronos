using UnityEngine;

namespace Unite.Enemies.Spawning
{
    /// <summary>
    /// Contains information about enemies to spawn during a wave, along with a set amount
    /// of currency to spend when spawning enemies.
    ///
    /// <seealso cref="EnemySpawnConfig"/>
    /// </summary>
    [System.Serializable]
    public class EnemyWave
    {
        [SerializeField]
        private EnemySpawnConfig[] enemySpawns;

        [SerializeField]
        private int currencyToSpend;

        public EnemySpawnConfig[] EnemySpawns => enemySpawns;
        public int CurrencyToSpend => currencyToSpend;
    }
}