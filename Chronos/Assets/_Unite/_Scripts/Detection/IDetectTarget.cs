using UnityEngine;

namespace Unite.Detection
{
    public interface IDetectTarget
    {
        public bool IsTargetDetected(Transform target);
    }
}