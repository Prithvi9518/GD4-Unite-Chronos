using UnityEngine;

namespace Unite.Enemies
{
    /// <summary>
    /// Runtime wrapper around the AttackData scriptable object
    /// to check and update the cooldown for the attack
    /// </summary>
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