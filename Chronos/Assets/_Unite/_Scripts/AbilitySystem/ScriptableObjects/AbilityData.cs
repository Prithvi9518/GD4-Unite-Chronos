using Unite.EventSystem;
using UnityEngine;

namespace Unite.AbilitySystem
{
    public abstract class AbilityData : ScriptableObject
    {
        [SerializeField] private string abilityName;
        [SerializeField] private Sprite sprite;
        [SerializeField] private float activeTimeMs;
        [SerializeField] private float cooldownTimeMs;
        [SerializeField] protected GameEvent onAbilityActivate;
        [SerializeField] protected GameEvent onAbilityDeactivate;

        public abstract void Activate();

        public abstract void Deactivate();

        public string GetAbilityName()
        {
            return abilityName;
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        public float ActiveTimeMs()
        {
            return activeTimeMs;
        }

        public float CooldownTimeMs()
        {
            return cooldownTimeMs;
        }
    }
}