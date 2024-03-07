using Unite.Detection;
using UnityEngine;

namespace Unite.Enemies.AI
{
    public class EnemyDetectionHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        private IDetectTarget detectionLogic;

        public Transform Target
        {
            get { return target; }
            set { target = value; }
        }

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