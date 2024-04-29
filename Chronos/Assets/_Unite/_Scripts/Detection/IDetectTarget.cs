using UnityEngine;

namespace Unite.Detection
{
    /// <summary>
    /// Any script used to detect a target based on certain logic must implement this interface.
    ///
    /// <seealso cref="LineOfSightDetection"/>
    /// </summary>
    public interface IDetectTarget
    {
        public bool IsTargetDetected(Transform target);
    }
}