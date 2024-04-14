using UnityEngine;

namespace Unite.Detection
{
    /// <summary>
    /// Used to detect whether a target is within the object's line of sight.
    /// Target and obstruction masks are used to make sure the target is only seen when
    /// the line of sight is not obstructed by any obstacles (e.g. walls, rocks, etc.)
    /// </summary>
    public class LineOfSightDetection : MonoBehaviour, IDetectTarget
    {
        [SerializeField]
        private float detectionRadius;

        [SerializeField]
        [Range(0, 360)]
        private float angleInDegrees;

        [SerializeField]
        private LayerMask targetMask;

        [SerializeField]
        private LayerMask obstructionMask;

        public float DetectionRadius => detectionRadius;
        public float AngleInDegrees => angleInDegrees;
        
        /// <summary>
        /// First, a sphere overlap is done to check whether the target is within the detected radius.
        /// 
        /// Second, a check is performed to see if the target is within the specified field of view.
        /// 
        /// Next, a raycast is sent towards the target's direction to check for any obstacles.
        /// 
        /// If the raycast finds no obstacles in between the object and the target, then it means the
        /// target has been detected.
        /// </summary>
        /// <param name="target">Target to find</param>
        /// <returns></returns>
        public bool IsTargetDetected(Transform target)
        {
            Collider[] results = new Collider[1];
            var size = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, results, targetMask);
            
            if (size <= 0) return false;
            
            Transform detectedTransform = results[0].transform;
            if (detectedTransform != target) return false;
            
            Vector3 targetDir = GetTargetDirectionNormalized(target);

            if (!(Vector3.Angle(transform.forward, targetDir) < angleInDegrees / 2)) return false;
            
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            return !Physics.Raycast(transform.position, targetDir, distanceToTarget, obstructionMask);
        }
        
        private Vector3 GetTargetDirectionNormalized(Transform target)
        {
            return (target.position - transform.position).normalized;
        }
    }
}