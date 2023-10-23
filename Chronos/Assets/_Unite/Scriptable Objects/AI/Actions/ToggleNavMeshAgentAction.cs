using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName ="ToggleNavMeshAgentAction", menuName ="Unite/Scriptable Objects/AI/Actions/Toggle Nav Mesh Agent")]
    public class ToggleNavMeshAgentAction : Action
    {
        [SerializeField]
        private bool agentEnabled;

        private EnemyStateMachine enemy;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            if(enemy == null)
            {
                enemy = stateMachine as EnemyStateMachine;
            }

            enemy.Agent.enabled = agentEnabled;
        }

        private void OnEnable()
        {
            enemy = null;
        }
    }
}

