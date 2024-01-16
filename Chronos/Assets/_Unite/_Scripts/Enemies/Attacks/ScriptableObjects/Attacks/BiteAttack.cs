using UnityEngine;

namespace Unite.Enemies
{
    [CreateAssetMenu(fileName = "BiteAttack", menuName = "Enemies/Attacks/Bite")]
    public class BiteAttack : AttackData
    {
        [SerializeField]
        private string animatorTriggerName;

        public override void Attack(Enemy enemy)
        {
            enemy.AnimationHandler.SetAnimationTrigger(animatorTriggerName, true);
        }

        public override bool CheckDealDamage(Enemy enemy)
        {
            return WithinAttackRange(enemy.transform, enemy.DetectionHandler.Target);
        }
    }
}