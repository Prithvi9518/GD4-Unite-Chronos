using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "ChasePlayerAction", menuName = "Unite/Scriptable Objects/AI/Actions/ChasePlayerAction")]
    public class ChasePlayerAction : Action
    {
        public override void ExecuteAction(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;
            enemy.Agent.SetDestination(enemy.DetectionHandler.Target.position);
        }
    }
}