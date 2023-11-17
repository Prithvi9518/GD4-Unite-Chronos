using UnityEngine;

namespace Unite.Enemies.AI
{
    public interface IDetectTarget
    {
        public bool IsTargetDetected(Transform target);
    }
}