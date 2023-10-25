using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "EnemyRadiusDetectionData", menuName = "Unite/Scriptable Objects/Enemies/Detection/Radius Detection")]
    public class EnemyRadiusDetectionData : EnemyDetectionData
    {
        [SerializeField]
        private float detectionRadius;

        private EnemyStateMachine enemyStateMachine;

        public override void StoreEnemyInfo(EnemyStateMachine enemy)
        {
            enemyStateMachine = enemy;
        }

        public override bool IsTargetDetected(Transform target)
        {
            return Vector3.Distance(enemyStateMachine.transform.position, target.position) <= detectionRadius;
        }
    }
}