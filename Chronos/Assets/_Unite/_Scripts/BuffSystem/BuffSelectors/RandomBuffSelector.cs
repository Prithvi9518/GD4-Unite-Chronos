using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.BuffSystem
{
    /// <summary>
    /// Randomly selects a buff from an array of prefabs
    /// </summary>
    public class RandomBuffSelector : BuffSelector
    {
        [SerializeField]
        private GameObject[] allBuffs;

        private List<GameObject> selectableBuffs;

        private void Start()
        {
            selectableBuffs = new List<GameObject>(allBuffs);
        }

        public override GameObject SelectBuff()
        {
            GameObject buff = selectableBuffs[Random.Range(0, selectableBuffs.Count)];
            selectableBuffs.Remove(buff);

            return buff;
        }
    }
}