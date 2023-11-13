using UnityEngine;

namespace Unite
{
    public interface IDetectTarget
    {
        public bool IsTargetDetected(Transform target);
    }
}