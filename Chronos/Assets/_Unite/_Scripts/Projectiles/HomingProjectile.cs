using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.Projectiles
{
    public class HomingProjectile : Projectile
    {
        [SerializeField]
        private AnimationCurve positionCurve;

        [SerializeField]
        private AnimationCurve noiseCurve;

        [SerializeField]
        private float homingMoveSpeed;

        [SerializeField]
        private Vector2 minNoise = new Vector2(-3f, -0.25f);
        
        [SerializeField]
        private Vector2 maxNoise = new Vector2(3f, 1f);

        [SerializeField]
        [Range(0,1)]
        private float endHomingTime;
        
        private bool isMoving;

        private float homingTime;
        private bool homingEnded;

        private Vector3 startPos;
        private Vector3 targetPos;
        
        private Vector3 noise;
        private Vector3 bulletDirectionVector;
        private Vector3 horizontalNoiseVector;
        private float noisePos;

        public override void Spawn()
        {
            homingTime = 0;
            homingEnded = false;
            isMoving = true;
            
            startPos = transform.position;
            targetPos = projectileTarget.position;
            
            noise = new Vector2(Random.Range(minNoise.x, maxNoise.x), Random.Range(minNoise.y, maxNoise.y));
            bulletDirectionVector = new Vector3(targetPos.x, targetPos.y + yOffset, targetPos.z);
            horizontalNoiseVector = Vector3.Cross(bulletDirectionVector, Vector3.up).normalized;
        }

        private void FixedUpdate()
        {
            if (!isMoving) return;

            if (homingTime >= 1 || homingEnded) return;

            if (homingTime >= endHomingTime)
            {
                homingEnded = true;
                DoHomingEndedActions();
            }
            
            noisePos = noiseCurve.Evaluate(homingTime);
                
            transform.position = Vector3.Lerp(startPos, projectileTarget.position + new Vector3(0, yOffset, 0), positionCurve.Evaluate(homingTime)) + 
                                 new Vector3(horizontalNoiseVector.x * noisePos * noise.x, noisePos * noise.y, horizontalNoiseVector.z * noisePos * noise.x);
            transform.LookAt(projectileTarget.position + new Vector3(0, yOffset, 0));
                
            homingTime += Time.deltaTime * homingMoveSpeed;
        }

        public override void HandleTimeStopEvent(bool isTimeStopped)
        {
            base.HandleTimeStopEvent(isTimeStopped);
            isMoving = !isTimeStopped;

            if (!isTimeStopped)
                DoHomingEndedActions();
        }

        private void DoHomingEndedActions()
        {
            homingEnded = true;
            Vector3 shootDir = (projectileTarget.position - transform.position + (Vector3.up * yOffset)).normalized;
            rb.AddForce(transform.forward * moveSpeed, ForceMode.VelocityChange);
        }
    }
}