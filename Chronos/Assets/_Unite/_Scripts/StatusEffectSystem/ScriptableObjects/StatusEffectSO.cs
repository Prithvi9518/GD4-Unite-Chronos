using UnityEngine;

namespace Unite.StatusEffectSystem
{
    [CreateAssetMenu(fileName = "StatusEffect", menuName = "StatusEffects/Status Effect SO")]
    public class StatusEffectSO : ScriptableObject
    {
        [SerializeField]
        private string effectName;
        
        [SerializeField]
        private float damageOverTime;

        [SerializeField]
        private float movementPenalty;
        
        [SerializeField]
        private float intervalInSeconds;
        
        [SerializeField]
        private float lifetimeInSeconds;

        [SerializeField]
        private GameObject effectParticles;

        public float DamageOverTime => damageOverTime;
        public float IntervalInSeconds => intervalInSeconds;
        public float LifetimeInSeconds => lifetimeInSeconds;
        public GameObject EffectParticles => effectParticles;
    }
}