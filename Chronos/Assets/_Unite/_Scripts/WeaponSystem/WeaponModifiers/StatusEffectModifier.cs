using Unite.StatusEffectSystem;

namespace Unite.WeaponSystem.Modifiers
{
    [System.Serializable]
    public class StatusEffectModifier : IGunModifier
    {
        private StatusEffectSO statusEffect;

        public StatusEffectModifier(StatusEffectSO statusEffect)
        {
            this.statusEffect = statusEffect;
        }
        public void Apply(GunData gun)
        {
            gun.DamageConfig.SetStatusEffect(statusEffect);
        }
    }
}