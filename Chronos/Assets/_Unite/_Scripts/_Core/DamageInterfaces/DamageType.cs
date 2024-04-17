namespace Unite.Core.DamageInterfaces
{
    /// <summary>
    /// Used to specify whether damage is dealt directly by an attack (Direct),
    /// or whether it is dealt passively, for example, through a poison status effect (Passive)
    /// </summary>
    public enum DamageType
    {
        Direct,
        Passive
    }
}