using Unite.ImpactSystem;

namespace Unite.WeaponSystem.Modifiers
{
    [System.Serializable]
    public class ImpactTypeModifier : IGunModifier
    {
        private ImpactType impactType;

        public ImpactTypeModifier(ImpactType impactType)
        {
            this.impactType = impactType;
        }

        public void Apply(GunData gun)
        {
            gun.SetImpactType(impactType);
        }
    }
}