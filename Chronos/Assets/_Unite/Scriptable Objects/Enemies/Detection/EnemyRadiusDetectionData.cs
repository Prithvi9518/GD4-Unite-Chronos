using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "EnemyRadiusDetectionData", menuName = "Unite/Scriptable Objects/Enemies/Detection/Radius Detection")]
    public class EnemyRadiusDetectionData : EnemyDetectionData
    {
        [SerializeField]
        private float detectionRadius;

        public override bool IsTargetDetected(EnemyDetectionHandler detectionHandler)
        {
            return Vector3.Distance(detectionHandler.transform.position, detectionHandler.Target.position) <= detectionRadius;
        }
    }
}