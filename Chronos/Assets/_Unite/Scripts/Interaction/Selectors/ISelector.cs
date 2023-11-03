using UnityEngine;

namespace Unite
{
    public interface ISelector
    {
        public void CheckSelection(Ray ray);
        public Transform GetSelection();
        public RaycastHit GetHitInfo();
    }
}

