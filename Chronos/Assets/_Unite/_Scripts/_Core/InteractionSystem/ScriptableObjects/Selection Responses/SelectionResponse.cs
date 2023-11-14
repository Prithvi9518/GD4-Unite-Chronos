using UnityEngine;

namespace Unite.Core.InteractionSystem
{
    public abstract class SelectionResponse : ScriptableObject
    {
        public abstract void OnSelect(Transform transform);
        public abstract void OnDeselect(Transform transform);
    }
}

