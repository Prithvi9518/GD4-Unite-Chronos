using System;
using Unite.Enemies.AI;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

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

        [SerializeField]
        private string strafeLeftAnim;
        [SerializeField]
        private string strafeRightAnim;

        private NavMeshAgent agent;

        private bool isStrafing;

        private float waitStrafeTime;

        private EnemyDetectionHandler detectionHandler;
        private EnemyAnimationHandler animationHandler;

        private int previousDirection = -1;
        private string selectedAnim;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            detectionHandler = GetComponent<EnemyDetectionHandler>();
            animationHandler = GetComponent<EnemyAnimationHandler>();
        }

        private void Update()
        {
            if (!isStrafing) return;
            
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
                {
                    strafePosition = strafeRightTransform.position;
                }

                // HandleStrafeAnimation(strafeDirection);

                agent.SetDestination(new Vector3(strafePosition.x, transform.position.y,
                    strafePosition.z));
                waitStrafeTime = strafeStartTime;
            }
            else
            {
                waitStrafeTime -= Time.deltaTime;
            }
        }

        private void HandleStrafeAnimation(int strafeDirection)
        {
            if (strafeDirection == previousDirection) return;
            
            selectedAnim = (strafeDirection == 0) ? strafeLeftAnim : strafeRightAnim;
            animationHandler.SetAnimationTrigger(selectedAnim, true);

            previousDirection = strafeDirection;
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