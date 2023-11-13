using UnityEngine;

namespace Unite
{
    public abstract class SelectionResponse : ScriptableObject
    {
        public abstract void OnSelect(Transform transform);
        public abstract void OnDeselect(Transform transform);
    }
}

