using UnityEngine;

namespace Unite.HighlightSystem
{
    public abstract class HighlightResponse : MonoBehaviour
    {
        public abstract void EnableHighlight();
        public abstract void DisableHighlight();
    }
}