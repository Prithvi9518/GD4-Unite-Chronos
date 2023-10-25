using UnityEngine;

namespace Unite
{
    public class DebugRadiusDetection : MonoBehaviour
    {
        [SerializeField]
        private float detectionRadius;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}

