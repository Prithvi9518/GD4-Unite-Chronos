using UnityEngine;

namespace Unite.WeaponSystem
{
    [CreateAssetMenu(fileName = "DamageConfig", menuName = "Weapons/Damage Config")]
    public class DamageConfig : ScriptableObject
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
    }
}