using UnityEngine;

namespace Unite
{
    public class Attack
    {
        private AttackData attackData;
        private float timeWhenLastAttacked;

        public Attack(AttackData attackData)
        {
            this.attackData = attackData;
        }

        public AttackData AttackData => attackData;

        public bool CanUseAttack()
        {
            return timeWhenLastAttacked + attackData.AttackCooldown < Time.time;
        }

        public bool IsTargetInAttackRange(EnemyDetectionHandler enemy)
        {
            return Vector3.Distance(enemy.transform.position, enemy.Target.position) <= attackData.AttackRange;
        }

        public void DoAttack(EnemyStateMachine enemy)
        {
            attackData.Attack(enemy);
            timeWhenLastAttacked = Time.time;
        }
    }
}