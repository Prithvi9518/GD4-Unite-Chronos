namespace Unite.StatusEffectSystem
{
    public interface IStatusEffectable
    {
        public void ApplyStatusEffect(StatusEffectSO statusEffect);
        public void RemoveEffect();
        public void HandleEffect();
    }
}