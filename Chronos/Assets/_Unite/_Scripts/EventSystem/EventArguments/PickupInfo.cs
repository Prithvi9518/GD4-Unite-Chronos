using UnityEngine;

namespace Unite.EventSystem
{
    public class PickupInfo
    {
        private Transform transform;
        private float zoomFactor;

        public PickupInfo(Transform transform, float zoomFactor= 0)
        {
            this.transform = transform;
            this.zoomFactor = zoomFactor;
        }

        public Transform Transform => transform;
        public float ZoomFactor => zoomFactor;
    }
}