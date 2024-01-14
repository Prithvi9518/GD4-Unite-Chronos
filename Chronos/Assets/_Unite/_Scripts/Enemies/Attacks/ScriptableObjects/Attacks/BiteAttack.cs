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
            return Vector3.Distance(enemy.transform.position, enemy.DetectionHandler.Target.position) <= attackRange;
        }
    }
}