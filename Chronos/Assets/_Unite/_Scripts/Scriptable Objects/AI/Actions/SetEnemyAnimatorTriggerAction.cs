using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "SetEnemyAnimatorTrigger", menuName = "Unite/Scriptable Objects/AI/Actions/SetEnemyAnimatorTrigger")]
    public class SetEnemyAnimatorTriggerAction : Action
    {
        [SerializeField]
        private string animationTriggerName;
        [SerializeField]
        private bool isActive;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;
            enemy.AnimationHandler.SetAnimationTrigger(animationTriggerName, isActive);
        }
    }
}

