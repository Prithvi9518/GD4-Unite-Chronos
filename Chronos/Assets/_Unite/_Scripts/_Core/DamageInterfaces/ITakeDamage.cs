namespace Unite.Core.DamageInterfaces
{
    public interface ITakeDamage
    {
        public void TakeDamage(float damage, IAttacker attacker, IDoDamage attack);
    }
}