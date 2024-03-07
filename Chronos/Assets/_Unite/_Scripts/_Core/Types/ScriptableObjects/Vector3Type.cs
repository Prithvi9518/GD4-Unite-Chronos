using UnityEngine;

namespace Unite.Core.Types
{
    [CreateAssetMenu(fileName = "Vector3Type", menuName = "Types/Vector3")]
    public class Vector3Type : ScriptableObject
    {
        private Vector3 value;

        public Vector3 Value
        {
            get => value;
            set => this.value = value;
        }
    }
}