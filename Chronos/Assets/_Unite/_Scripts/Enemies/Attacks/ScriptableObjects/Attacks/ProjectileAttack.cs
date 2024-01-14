using UnityEngine;

namespace Unite.Enemies
{
    [CreateAssetMenu(fileName = "ProjectileAttack", menuName = "Enemies/Attacks/Projectile")]
    public class ProjectileAttack : AttackData
    {
        public override void Attack(Enemy enemy)
        {
            Debug.Log("Attack");
        }

        public override bool CheckDealDamage(Enemy enemy)
        {
            return true;
        }
    }
}