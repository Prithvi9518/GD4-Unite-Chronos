using Unite.StatePattern;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "ChaseCircleTargetAction", menuName = "AI/Actions/ChaseCircleTargetAction")]
    public class ChaseCircleTargetAction : FSMAction
    {
        [SerializeField]
        private float circleRadius;
        
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            NavMeshAgent agent = baseStateMachine.GetComponent<NavMeshAgent>();
            EnemyDetectionHandler detectionHandler = baseStateMachine.GetComponent<EnemyDetectionHandler>();
            Transform target = detectionHandler.Target;

            Vector2 circlePos = GetRandomCirclePosition(target);
            Vector3 destination = new Vector3(circlePos.x, baseStateMachine.transform.position.y, circlePos.y);

            agent.SetDestination(destination);
        }

        private Vector2 GetRandomCirclePosition(Transform target)
        {
            float rand = Random.value;
            Vector3 targetPos = target.position;

            float angle = rand * Mathf.PI * 2;
            float x = targetPos.x + Mathf.Cos(angle) * circleRadius;
            float z = targetPos.z + Mathf.Sin(angle) * circleRadius;

            return new Vector2(x, z);
        }
    }
}