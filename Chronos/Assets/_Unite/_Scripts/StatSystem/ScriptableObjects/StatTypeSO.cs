using UnityEngine;

namespace Unite.StatSystem
{
    [CreateAssetMenu(fileName = "StatType", menuName = "Stat Type")]
    public class StatTypeSO : ScriptableObject
    {
        [SerializeField] 
        private string statName;

        public string StatName => statName;
    }
}

