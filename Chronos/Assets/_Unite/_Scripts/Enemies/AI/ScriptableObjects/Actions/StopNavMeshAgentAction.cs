using Unite.StatePattern;
using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName ="StopNavMeshAgent", menuName ="AI/Actions/StopNavMeshAgent")]
    public class StopNavMeshAgentAction : FSMAction
    {
        [SerializeField]
        private bool isStopped;

        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            NavMeshAgent agent = baseStateMachine.GetComponent<NavMeshAgent>();
            agent.isStopped = isStopped;
        }
    }
}

