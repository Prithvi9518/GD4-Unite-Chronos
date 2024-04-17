using System.Collections.Generic;
using Unite.BuffSystem.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.BuffSystem
{
    /// <summary>
    /// Picks buffs based on a weighted priority which is set in the buff spawn configuration.
    ///
    /// <seealso cref="BuffSpawnSO"/>
    /// </summary>
    public class WeightedRandomBuffSelector : BuffSelector
    {
        [SerializeField] 
        private List<BuffSpawnSO> buffSpawnConfigs;

        private List<BuffSpawnConfig> selectableBuffConfigs;
        private List<float> weights;

        private void Awake()
        {
            selectableBuffConfigs = new();
            foreach(var buffSpawn in buffSpawnConfigs)
            {
                selectableBuffConfigs.Add(new BuffSpawnConfig(buffSpawn));
            }
            
            weights = new();
        }

        public override GameObject SelectBuff()
        {
            ResetWeights();

            float value = Random.value;
            
            for (int i = 0; i < weights.Count; i++)
            {
                if (!(value < weights[i])) continue;
                
                BuffSpawnConfig buffSpawnConfig = selectableBuffConfigs[i];
                if (buffSpawnConfig.CanSpawn)
                {
                    GameObject buffToSpawn = buffSpawnConfig.BuffSpawn.BuffPrefab;
                    buffSpawnConfig.UpdateCanSpawn();
                    return buffToSpawn;
                }

                value -= weights[i];
                selectableBuffConfigs.Remove(buffSpawnConfig);
            }

            foreach (var buffConfig in selectableBuffConfigs)
            {
                if(!buffConfig.CanSpawn) continue;
                GameObject buffToSpawn = buffConfig.BuffSpawn.BuffPrefab;
                buffConfig.UpdateCanSpawn();
                return buffToSpawn;
            }

            return null;
        }

        private void ResetWeights()
        {
            weights.Clear();
            float totalWeight = 0;
            
            for (int i = 0; i < selectableBuffConfigs.Count; i++)
            {
                float weight = selectableBuffConfigs[i].BuffSpawn.GetWeight();
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