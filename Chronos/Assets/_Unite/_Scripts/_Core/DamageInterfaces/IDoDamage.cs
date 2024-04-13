namespace Unite.Core.DamageInterfaces
{
    /// <summary>
    /// Used for analytics purposes to get the name of the weapon/attack dealing damage and send
    /// the data using Unity Analytics.
    /// Implementing the IDoDamage interface on the weapon/attack allows
    /// the access of the name and damage type (Direct/Passive) when damage is dealt.
    /// </summary>
    public interface IDoDamage
    {
        public string GetName();

        public DamageType GetDamageType();
    }
}