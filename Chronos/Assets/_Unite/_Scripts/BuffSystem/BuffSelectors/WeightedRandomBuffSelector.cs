using System.Collections.Generic;
using Unite.BuffSystem.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.BuffSystem
{
    public class WeightedRandomBuffSelector : BuffSelector
    {
        [SerializeField] 
        private List<BuffSpawnSO> buffSpawnConfigs;

        private BuffSpawnConfig[] selectableBuffConfigs;
        private float[] weights;

        private void Awake()
        {
            selectableBuffConfigs = new BuffSpawnConfig[buffSpawnConfigs.Count];
            for(int i = 0; i < selectableBuffConfigs.Length; i++)
            {
                selectableBuffConfigs[i] = new BuffSpawnConfig(buffSpawnConfigs[i]);
            }
            
            weights = new float[selectableBuffConfigs.Length];
        }

        public override GameObject SelectBuff()
        {
            ResetWeights();

            float value = Random.value;
            
            for (int i = 0; i < weights.Length; i++)
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
            float totalWeight = 0;
            for (int i = 0; i < selectableBuffConfigs.Length; i++)
            {
                weights[i] = selectableBuffConfigs[i].BuffSpawn.GetWeight();
                totalWeight += weights[i];
            }

            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] /= totalWeight;
            }
        }
    }
}