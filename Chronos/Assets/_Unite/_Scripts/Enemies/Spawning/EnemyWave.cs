using UnityEngine;

namespace Unite.Enemies.Spawning
{
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