using UnityEngine;

namespace Unite.StatSystem
{
    [System.Serializable]
    public class StatModifier
    {
        [SerializeField]
        private StatModifierType modifierType;
        
        [SerializeField]
        private float value;

        public StatModifier(StatModifierType modifierType, float value)
        {
            this.modifierType = modifierType;
            this.value = value;
        }

        public float Value => value;
        public StatModifierType ModifierType => modifierType;

        public float ApplyToStatValue(float baseValue)
        {
            float finalValue = baseValue;
            switch (modifierType)
            {
                case StatModifierType.Flat:
                    finalValue += value;
                    break;
                case StatModifierType.Percentage:
                    finalValue *= (value / 100f) + 1f;
                    break;
            }

            return finalValue;
        }
    }
}