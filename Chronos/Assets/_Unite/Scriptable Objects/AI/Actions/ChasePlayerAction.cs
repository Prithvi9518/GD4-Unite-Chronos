using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "ChasePlayerAction", menuName = "Unite/Scriptable Objects/AI/Actions/ChasePlayerAction")]
    public class ChasePlayerAction : Action
    {
        private EnemyStateMachine enemy;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            if (enemy == null)
            {
                enemy = stateMachine as EnemyStateMachine;
            }
            enemy.Agent.SetDestination(enemy.Target.transform.position);
        }
    }
}