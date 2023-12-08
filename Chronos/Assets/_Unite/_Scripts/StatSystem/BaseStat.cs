using UnityEngine;

namespace Unite.StatSystem
{
    [System.Serializable]
    public class BaseStat
    {
        [SerializeField]
        private StatTypeSO statType;
        [SerializeField]
        private float value;

        public StatTypeSO StatType => statType;
        public float Value => value;
    }
}