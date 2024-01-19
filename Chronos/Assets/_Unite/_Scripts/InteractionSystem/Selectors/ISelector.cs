using UnityEngine;

namespace Unite.InteractionSystem
{
    public interface ISelector
    {
        public void CheckSelection(Ray ray);
        public Transform GetSelection();
        public RaycastHit GetHitInfo();
    }
}

