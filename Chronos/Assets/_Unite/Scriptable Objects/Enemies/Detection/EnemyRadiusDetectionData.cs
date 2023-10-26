using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "EnemyRadiusDetectionData", menuName = "Unite/Scriptable Objects/Enemies/Detection/Radius Detection")]
    public class EnemyRadiusDetectionData : EnemyDetectionData
    {
        [SerializeField]
        private float detectionRadius;

        public override bool IsTargetDetected(EnemyStateMachine enemy)
        {
            return Vector3.Distance(enemy.transform.position, enemy.Target.position) <= detectionRadius;
        }
    }
}