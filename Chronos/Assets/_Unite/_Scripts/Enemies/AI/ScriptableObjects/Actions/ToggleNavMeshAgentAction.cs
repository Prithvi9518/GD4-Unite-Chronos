using Unite.StatePattern;
using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName ="ToggleNavMeshAgentAction", menuName ="AI/Actions/Toggle Nav Mesh Agent")]
    public class ToggleNavMeshAgentAction : FSMAction
    {
        [SerializeField]
        private bool agentEnabled;

        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            NavMeshAgent agent = baseStateMachine.GetComponent<NavMeshAgent>();
            agent.enabled = agentEnabled;
        }
    }
}

