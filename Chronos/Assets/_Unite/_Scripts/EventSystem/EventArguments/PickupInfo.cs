using UnityEngine;

namespace Unite.EventSystem
{
    public class PickupInfo
    {
        private Transform transform;
        private float zoomFactor;
        private bool isRotationDisabled;

        public PickupInfo(Transform transform, float zoomFactor= 0, bool disableRotation=false)
        {
            this.transform = transform;
            this.zoomFactor = zoomFactor;
            this.isRotationDisabled = disableRotation;
        }

        public Transform Transform => transform;
        public float ZoomFactor => zoomFactor;
        public bool IsRotationDisabled => isRotationDisabled;
    }
}