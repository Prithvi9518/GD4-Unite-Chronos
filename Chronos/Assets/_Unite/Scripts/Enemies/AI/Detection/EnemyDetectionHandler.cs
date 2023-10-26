using UnityEngine;

namespace Unite
{
    public class EnemyDetectionHandler : MonoBehaviour, IDetectTarget
    {
        private EnemyDetectionData detectionData;
        private EnemyStateMachine enemyStateMachine;

        private void Awake()
        {
            enemyStateMachine = GetComponent<EnemyStateMachine>();
        }

        public void SetupDetectionData(EnemyDetectionData data)
        {
            detectionData = data;
        }

        public bool IsTargetDetected()
        {
            return detectionData.IsTargetDetected(enemyStateMachine);
        }
    }
}