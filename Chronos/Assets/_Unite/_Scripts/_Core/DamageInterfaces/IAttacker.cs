using UnityEngine;

namespace Unite.Core.DamageInterfaces
{
    public interface IAttacker
    {
        public string GetName();

        public Transform GetTransform();
    }
}