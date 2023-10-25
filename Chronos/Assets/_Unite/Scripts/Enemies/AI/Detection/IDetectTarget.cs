using UnityEngine;

namespace Unite
{
    /// <summary>
    /// Use this interface to create various types of detection logic.
    /// For example: detection through line of sight, detection when target is within a radius, etc.
    /// </summary>
    public interface IDetectTarget
    {
        public bool IsTargetDetected(Transform target);
    }
}