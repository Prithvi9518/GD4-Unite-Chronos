using System.Collections.Generic;

namespace Unite.StatSystem
{
    [System.Serializable]
    public class Stat
    {
        private float baseValue;
        private float value;
        
        private List<StatModifier> modifiers;

        private bool isDirty = true;

        public Stat(float value)
        {
            baseValue = value;
            modifiers = new List<StatModifier>();
        }

        public float Value
        {
            get
            {
                if (!isDirty) return value;
                
                CalculateValue();
                isDirty = false;
                return value;
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            isDirty = true;
            modifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            isDirty = true;
            modifiers.Remove(modifier);
        }

        private void CalculateValue()
        {
            float resultingValue = baseValue;
            
            foreach(var modifier in modifiers)
            {
                resultingValue = modifier.ApplyToStatValue(resultingValue);
            }

            value = resultingValue;
        }
    }
}