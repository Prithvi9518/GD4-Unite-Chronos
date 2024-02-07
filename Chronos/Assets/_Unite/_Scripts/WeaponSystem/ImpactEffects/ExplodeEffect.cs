using Unite.Core.DamageInterfaces;
using UnityEngine;

namespace Unite.WeaponSystem.ImpactEffects
{
    public class ExplodeEffect : IImpactHandler
    {
        public float Radius { get; }
        public AnimationCurve DamageFalloff { get; }
        public int BaseExplosionDamage { get; }
        public int MaxEnemiesAffected { get; }

        private Collider[] hitObjects;

        public ExplodeEffect(float radius, AnimationCurve damageFalloff, int baseExplosionDamage,
            int maxEnemiesAffected)
        {
            this.Radius = radius;
            this.DamageFalloff = damageFalloff;
            this.BaseExplosionDamage = baseExplosionDamage;
            this.MaxEnemiesAffected = maxEnemiesAffected;
            
            hitObjects = new Collider[maxEnemiesAffected];
        }
        
        public void HandleImpact(Collider impactedObject, Vector3 hitPos, Vector3 hitNormal, GunData gun)
        {
            int hits = Physics.OverlapSphereNonAlloc(hitPos,
                Radius, hitObjects, gun.ShootData.HitMask);

            for (int i = 0; i < hits; i++)
            {
                if (hitObjects[i].TryGetComponent(out ITakeDamage damageable))
                {
                    float distance = Vector3.Distance(hitPos, hitObjects[i].ClosestPoint(hitPos));
                    
                    float damage = BaseExplosionDamage * DamageFalloff.Evaluate(distance / Radius);
                    damageable.TakeDamage(damage + gun.BaseDamage, gun.Shooter, gun);
                }
            }
        }
    }
}