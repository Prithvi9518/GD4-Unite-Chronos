using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "BiteAttack", menuName = "Unite/Scriptable Objects/Enemies/Attacks/Bite")]
    public class BiteAttack : AttackData
    {
        [SerializeField]
        private string animatorTriggerName;

        public override void Attack(EnemyStateMachine enemy)
        {
            enemy.AnimationHandler.SetAnimationTrigger(animatorTriggerName, true);
        }
    }
}