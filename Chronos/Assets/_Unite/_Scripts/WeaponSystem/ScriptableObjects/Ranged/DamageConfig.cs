using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.WeaponSystem
{
    [CreateAssetMenu(fileName = "DamageConfig", menuName = "Weapons/Damage Config")]
    public class DamageConfig : ScriptableObject, ICloneable
    {
        [SerializeField] 
        private ParticleSystem.MinMaxCurve damageCurve;

        private void Reset()
        {
            damageCurve.mode = ParticleSystemCurveMode.Curve;
        }

        public float GetDamage(float distance = 0)
        {
            return damageCurve.Evaluate(distance, Random.value);
        }

        public object Clone()
        {
            DamageConfig config = CreateInstance<DamageConfig>();

            config.damageCurve = damageCurve;
            return config;
        }
    }
}