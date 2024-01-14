using Unite.Enemies.AI;
using UnityEngine;

namespace Unite.Enemies
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

        public void DoAttack(Enemy enemy)
        {
            attackData.Attack(enemy);
            timeWhenLastAttacked = Time.time;
        }

        public bool CheckDealDamage(Enemy enemy)
        {
            return attackData.CheckDealDamage(enemy);
        }
    }
}