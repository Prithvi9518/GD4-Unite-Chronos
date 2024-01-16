using UnityEngine;

namespace Unite.BuffSystem
{
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