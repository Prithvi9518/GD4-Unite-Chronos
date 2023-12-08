using UnityEngine;

namespace Unite.StatSystem
{
    [CreateAssetMenu(fileName = "StatType", menuName = "Stats/Stat Type")]
    public class StatTypeSO : ScriptableObject
    {
        [SerializeField] 
        private string statName;

        public string StatName => statName;
    }
}

