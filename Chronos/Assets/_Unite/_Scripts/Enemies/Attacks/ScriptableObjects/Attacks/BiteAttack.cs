using Unite.Enemies.AI;
using UnityEngine;

namespace Unite.Enemies
{
    [CreateAssetMenu(fileName = "BiteAttack", menuName = "Enemies/Attacks/Bite")]
    public class BiteAttack : AttackData
    {
        [SerializeField]
        private string animatorTriggerName;

        public override void Attack(EnemyStateMachine enemy)
        {
            enemy.AnimationHandler.SetAnimationTrigger(animatorTriggerName, true);
        }

        public override bool CheckDealDamage(EnemyStateMachine enemy)
        {
            return Vector3.Distance(enemy.transform.position, enemy.DetectionHandler.Target.position) <= attackRange;
        }
    }
}