using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName ="StopNavMeshAgent", menuName ="AI/Actions/StopNavMeshAgent")]
    public class StopNavMeshAgentAction : Action
    {
        [SerializeField]
        private bool isStopped;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;
            enemy.Agent.isStopped = isStopped;
        }
    }
}

