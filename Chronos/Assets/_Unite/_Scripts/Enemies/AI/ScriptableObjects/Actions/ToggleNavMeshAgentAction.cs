using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName ="ToggleNavMeshAgentAction", menuName ="AI/Actions/Toggle Nav Mesh Agent")]
    public class ToggleNavMeshAgentAction : Action
    {
        [SerializeField]
        private bool agentEnabled;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;
            enemy.Agent.enabled = agentEnabled;
        }
    }
}

