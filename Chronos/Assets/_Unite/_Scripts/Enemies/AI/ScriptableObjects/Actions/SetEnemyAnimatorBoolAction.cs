using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName ="SetEnemyAnimatorBool", menuName ="AI/Actions/SetEnemyAnimatorBool")]
    public class SetEnemyAnimatorBoolAction : FSMAction
    {
        [SerializeField]
        private string animatorBoolName;
        [SerializeField]
        private bool value;

        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            EnemyAnimationHandler animationHandler = baseStateMachine.GetComponent<EnemyAnimationHandler>();
            animationHandler.SetAnimationBool(animatorBoolName, value);
        }
    }
}

