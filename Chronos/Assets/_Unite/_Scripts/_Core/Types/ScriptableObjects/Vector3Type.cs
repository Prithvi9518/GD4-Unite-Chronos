using UnityEngine;

namespace Unite.Core.Types
{
    /// <summary>
    /// Stores a reference to a Vector3 value that can be updated from
    /// other classes having a reference to the scriptable object.
    /// </summary>
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