using UnityEngine;

namespace Unite
{
    public class RadiusDetection : MonoBehaviour, IDetectTarget
    {
        [SerializeField]
        private float detectionRadius;

        public bool IsTargetDetected(Transform target)
        {
            return Vector3.Distance(transform.position, target.position) <= detectionRadius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}

