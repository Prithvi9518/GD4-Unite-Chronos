using Unite.Core.DamageInterfaces;

namespace Unite.StatusEffectSystem
{
    public interface IStatusEffectable
    {
        public void ApplyStatusEffect(StatusEffectSO statusEffect, IAttacker attacker);
        public void RemoveEffect();
        public void HandleEffect();
    }
}