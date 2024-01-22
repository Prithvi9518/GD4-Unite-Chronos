using Unite.Core.DamageInterfaces;
using Unite.StatusEffectSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerStatusEffectable : MonoBehaviour, IStatusEffectable
    {
        private PlayerHealthHandler healthHandler;
        
        private StatusEffectSO effectData;
        private IAttacker effectUser;
        
        private float timeSinceEffectApplied;
        private float nextEffectUpdateTime;
        
        private void Update()
        {
            if (effectData == null) return;
            HandleEffect();
        }
        
        public void ApplyStatusEffect(StatusEffectSO statusEffect, IAttacker attacker)
        {
            RemoveEffect();
            effectUser = attacker;
            effectData = statusEffect;
        }

        public void RemoveEffect()
        {
            effectData = null;
        }

        public void HandleEffect()
        {
            timeSinceEffectApplied += Time.deltaTime;
            if (timeSinceEffectApplied >= effectData.LifetimeInSeconds)
            {
                RemoveEffect();
                return;
            }

            if (effectData.DamageOverTime == 0 ||
                timeSinceEffectApplied < nextEffectUpdateTime) return;
            
            nextEffectUpdateTime += effectData.IntervalInSeconds;
            healthHandler.TakeDamage(effectData.DamageOverTime, effectUser, effectData);
        }

        public void PerformSetup(Player p)
        {
            healthHandler = p.HealthHandler;
        }
    }
}