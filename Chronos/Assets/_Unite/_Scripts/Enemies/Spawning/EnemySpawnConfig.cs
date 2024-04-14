using UnityEngine;

namespace Unite.Enemies.Spawning
{
    /// <summary>
    /// Assigns weights to enemies - this is used to prioritize certain enemies
    /// during spawning.
    /// </summary>
    [System.Serializable]
    public class EnemySpawnConfig
    {
        [SerializeField]
        private EnemyData enemyData;
        
        [SerializeField]
        [Range(0,1)]
        private float minWeight;
        
        [SerializeField]
        [Range(0,1)]
        private float maxWeight;

        public EnemyData EnemyData => enemyData;
        public float MinWeight => minWeight;
        public float MaxWeight => maxWeight;
        
        public float GetWeight()
        {
            return Random.Range(minWeight, maxWeight);
        }
    }
}