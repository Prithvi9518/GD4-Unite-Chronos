using UnityEngine;

namespace Unite.Core.DamageInterfaces
{
    /// <summary>
    /// Used for analytics purposes to get the name of the entity dealing damage and send
    /// the data using Unity Analytics.
    /// Implementing the IAttacker interface on the attacking entity allows
    /// the access of the name and transform when damage is dealt.
    /// </summary>
    public interface IAttacker
    {
        public string GetName();

        public Transform GetTransform();
    }
}