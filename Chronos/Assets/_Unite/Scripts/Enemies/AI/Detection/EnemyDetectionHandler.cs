using UnityEngine;

namespace Unite
{
    public class EnemyDetectionHandler : MonoBehaviour, IDetectTarget
    {
        [SerializeField]
        private Transform target;

        private EnemyDetectionData detectionData;

        public Transform Target => target;

        public void SetupDetectionData(EnemyDetectionData data)
        {
            detectionData = data;
        }

        public bool IsTargetDetected()
        {
            return detectionData.IsTargetDetected(this);
        }
    }
}