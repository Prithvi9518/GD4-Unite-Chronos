using Unite.Enemies.AI;
using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.Movement
{
    public class StrafeHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform strafeLeftTransform;

        [SerializeField]
        private Transform strafeRightTransform;

        [SerializeField]
        private float minStrafeTime;
        
        [SerializeField]
        private float maxStrafeTime;

        [SerializeField]
        private float lookRotationFactor;

        private NavMeshAgent agent;

        private bool isStrafing;

        private float waitStrafeTime;

        private EnemyDetectionHandler detectionHandler;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            detectionHandler = GetComponent<EnemyDetectionHandler>();
        }

        private void Update()
        {
            if (!isStrafing) return;
            
            // Debug.Log("strafing");
            Strafe();
            FaceTarget(detectionHandler.Target);
        }

        private void Strafe()
        {
            int strafeDirection = Random.Range(0, 2);
            float strafeStartTime = Random.Range(minStrafeTime, maxStrafeTime);

            if (waitStrafeTime <= 0)
            {
                Vector3 strafePosition = strafeLeftTransform.position;
                if (strafeDirection == 1)
                    strafePosition = strafeRightTransform.position;
                
                agent.SetDestination(new Vector3(strafePosition.x, transform.position.y,
                    strafePosition.z));
                waitStrafeTime = strafeStartTime;
            }
            else
            {
                waitStrafeTime -= Time.deltaTime;
            }
        }

        private void FaceTarget(Transform target)
        {
            Vector3 lookDir = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(lookDir.x, 0, lookDir.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationFactor);
        }

        public void ToggleStrafing(bool active)
        {
            isStrafing = active;
        }
    }
}