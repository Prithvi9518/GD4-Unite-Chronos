using UnityEngine;

namespace Unite.Enemies.AI
{
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