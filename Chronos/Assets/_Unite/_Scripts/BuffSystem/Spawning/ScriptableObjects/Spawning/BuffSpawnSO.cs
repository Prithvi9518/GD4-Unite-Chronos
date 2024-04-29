using UnityEngine;

namespace Unite.BuffSystem
{
    /// <summary>
    /// ScriptableObject to store a buff's spawn configuration.
    /// This includes the buff's priority/weight, and the number of times the buff can be spawned.
    ///
    /// Used when a buff is selected in one of the buff selector classes.
    /// <seealso cref="WeightedRandomBuffSelector"/>
    /// <seealso cref="BuffSpawnManager"/>
    /// </summary>
    [CreateAssetMenu(fileName = "BuffSpawnSO", menuName = "Buffs/BuffSpawnSO")]
    public class BuffSpawnSO : ScriptableObject
    {
        [SerializeField]
        private GameObject buffPrefab;

        [SerializeField] 
        private bool unlimitedSpawning;

        [SerializeField] 
        private int spawnLimit;

        [SerializeField]
        [Range(0,1)]
        private float minWeight;
        
        [SerializeField]
        [Range(0,1)]
        private float maxWeight;

        public GameObject BuffPrefab => buffPrefab;
        public bool UnlimitedSpawning => unlimitedSpawning;
        public int SpawnLimit => spawnLimit;
        public float MinWeight => minWeight;
        public float MaxWeight => maxWeight;

        public float GetWeight()
        {
            return Random.Range(minWeight, maxWeight);
        }
    }
}