using Unite.StatePattern;
using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.AI
{
    /// <summary>
    /// Action used by the enemy state machine to chase the target
    /// </summary>
    [CreateAssetMenu(fileName = "ChasePlayerAction", menuName = "AI/Actions/ChasePlayerAction")]
    public class ChasePlayerAction : FSMAction
    {
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            NavMeshAgent agent = baseStateMachine.GetComponent<NavMeshAgent>();
            EnemyDetectionHandler detectionHandler = baseStateMachine.GetComponent<EnemyDetectionHandler>();
            
            agent.SetDestination(detectionHandler.Target.position);
        }
    }
}