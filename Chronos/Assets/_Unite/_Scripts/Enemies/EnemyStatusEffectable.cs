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
        }
    }
}