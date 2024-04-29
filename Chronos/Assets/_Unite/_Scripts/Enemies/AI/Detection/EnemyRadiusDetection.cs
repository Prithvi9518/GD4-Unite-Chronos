using Unite.Detection;
using UnityEngine;


namespace Unite.Enemies.AI
{
    /// <summary>
    /// Detection logic to search for the target within a specified radius.
    /// </summary>
    public class EnemyRadiusDetection : MonoBehaviour, IDetectTarget
    {
        [SerializeField]
        private float detectionRadius;

        public bool IsTargetDetected(Transform target)
        {
            return Vector3.Distance(transform.position, target.position) <= detectionRadius;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}

