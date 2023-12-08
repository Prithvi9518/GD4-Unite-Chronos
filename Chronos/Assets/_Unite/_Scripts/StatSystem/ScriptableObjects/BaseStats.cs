using System.Collections.Generic;
using UnityEngine;

namespace Unite.StatSystem
{
    [CreateAssetMenu(fileName = "BaseStats", menuName = "Stats/Base Stats")]
    public class BaseStats : ScriptableObject
    {
        [SerializeField] 
        private List<BaseStat> baseStats;

        public List<BaseStat> Stats => baseStats;
    }
}