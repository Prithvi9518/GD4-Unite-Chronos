using UnityEngine;

namespace Unite.Enemies.Spawning
{
    [System.Serializable]
    public class EnemyWave
    {
        [SerializeField]
        private EnemyData[] enemies;

        [SerializeField]
        private int currencyToSpend;

        public EnemyData[] Enemies => enemies;
        public int CurrencyToSpend => currencyToSpend;
    }
}