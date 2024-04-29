namespace Unite.Core.DamageInterfaces
{
    /// <summary>
    /// Any entity that can take damage must implement this interface.
    /// Concrete implementations of the TakeDamage method can specify how the damage is dealt.
    /// </summary>
    public interface ITakeDamage
    {
        /// <param name="damage">Total damage dealt by the attacking entity</param>
        /// <param name="attacker">The attacking entity</param>
        /// <param name="attack">The weapon/skill/attack used to deal damage</param>
        public void TakeDamage(float damage, IAttacker attacker, IDoDamage attack);
    }
}