using UnityEngine;

namespace Unite
{
    public class EnemyDetectionHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        private IDetectTarget detectionLogic;

        public Transform Target => target;

        private void Awake()
        {
            detectionLogic = GetComponent<IDetectTarget>();
        }

        public bool IsTargetDetected()
        {
            return detectionLogic.IsTargetDetected(target);
        }
    }
}