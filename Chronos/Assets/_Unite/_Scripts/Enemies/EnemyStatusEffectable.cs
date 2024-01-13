using Unite.Core;
using Unite.StatusEffectSystem;
using UnityEngine;

namespace Unite.Enemies
{
    public class EnemyStatusEffectable : MonoBehaviour, IStatusEffectable
    {
        [SerializeField]
        private Transform particleParentTransform;
        
        private StatusEffectSO effectData;
        private GameObject effectParticles;

        private float timeSinceEffectApplied;
        private float nextEffectUpdateTime;

        private EnemyDamager damager;

        private void Awake()
        {
            damager = GetComponent<EnemyDamager>();
        }

        private void Update()
        {
            if (effectData == null) return;
            HandleEffect();
        }

        public void ApplyStatusEffect(StatusEffectSO statusEffect)
        {
            RemoveEffect();
            effectData = statusEffect;
            effectParticles = Instantiate(effectData.EffectParticles, particleParentTransform);
        }

        public void RemoveEffect()
        {
            effectData = null;
            Destroy(effectParticles);
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
            damager.TakeDamage(effectData.DamageOverTime);
        }
    }
}