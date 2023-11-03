using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName ="SetEnemyAnimatorBool", menuName ="Unite/Scriptable Objects/AI/Actions/SetEnemyAnimatorBool")]
    public class SetEnemyAnimatorBoolAction : Action
    {
        [SerializeField]
        private string animatorBoolName;
        [SerializeField]
        private bool value;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;
            enemy.AnimationHandler.SetAnimationBool(animatorBoolName, value);
        }
    }
}

