using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "SetEnemyAnimatorTrigger", menuName = "AI/Actions/SetEnemyAnimatorTrigger")]
    public class SetEnemyAnimatorTriggerAction : Action
    {
        [SerializeField]
        private string animationTriggerName;
        [SerializeField]
        private bool isActive;

        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            EnemyAnimationHandler animationHandler = baseStateMachine.GetComponent<EnemyAnimationHandler>();
            animationHandler.SetAnimationTrigger(animationTriggerName, isActive);
        }
    }
}

