using UnityEngine;

namespace Unite.Core.StatSystem
{
    [System.Serializable]
    public class StatInfo
    {
        [SerializeField] 
        private StatTypeSO statType;

        [SerializeField] 
        private float statValue;

        public StatTypeSO StatType => statType;

        public float Value
        {
            get => statValue;
            set => statValue = value;
        }
    }
}