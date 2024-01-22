using Unite.Core.DamageInterfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Unite.StatusEffectSystem
{
    [CreateAssetMenu(fileName = "StatusEffect", menuName = "StatusEffects/Status Effect SO")]
    public class StatusEffectSO : ScriptableObject, IDoDamage
    {
        [SerializeField]
        private string effectName;
        
        [SerializeField]
        private float damageOverTime;

        [SerializeField]
        private float slowdownPenalty;
        
        [SerializeField]
        private float intervalInSeconds;
        
        [SerializeField]
        private float lifetimeInSeconds;

        [SerializeField]
        private GameObject effectParticles;

        public float DamageOverTime => damageOverTime;
        public float SlowdownPenalty => slowdownPenalty;
        public float IntervalInSeconds => intervalInSeconds;
        public float LifetimeInSeconds => lifetimeInSeconds;
        public GameObject EffectParticles => effectParticles;
        public string GetName()
        {
            return effectName;
        }
    }
}