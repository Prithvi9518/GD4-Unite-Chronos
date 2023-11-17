using UnityEngine;

namespace Unite.Core.InteractionSystem
{
    public interface ISelector
    {
        public void CheckSelection(Ray ray);
        public Transform GetSelection();
        public RaycastHit GetHitInfo();
    }
}

