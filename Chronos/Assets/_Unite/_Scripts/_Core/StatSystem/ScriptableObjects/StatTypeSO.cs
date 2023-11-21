using UnityEngine;

namespace Unite.Core.StatSystem
{
    [CreateAssetMenu(fileName = "StatType", menuName = "Unite/Scriptable Objects/Stat Type")]
    public class StatTypeSO : ScriptableObject
    {
        [SerializeField] 
        private string statName;

        public string StatName => statName;
    }
}

