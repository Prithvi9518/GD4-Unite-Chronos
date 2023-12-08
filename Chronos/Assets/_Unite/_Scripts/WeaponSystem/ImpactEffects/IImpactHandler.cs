using UnityEngine;

namespace Unite.WeaponSystem.ImpactEffects
{
    public interface IImpactHandler
    {
        public void HandleImpact(Collider impactedObject,
            Vector3 hitPos, Vector3 hitNormal, GunData gun);
    }
}