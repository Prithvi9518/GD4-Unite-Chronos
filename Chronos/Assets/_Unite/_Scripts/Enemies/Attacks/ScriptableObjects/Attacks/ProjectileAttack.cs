using Unite.Projectiles;
using UnityEngine;

namespace Unite.Enemies
{
    [CreateAssetMenu(fileName = "ProjectileAttack", menuName = "Enemies/Attacks/Projectile")]
    public class ProjectileAttack : AttackData
    {
        [SerializeField]
        private Projectile projectilePrefab;

        public Projectile ProjectilePrefab => projectilePrefab;

        public override void Attack(Enemy enemy)
        {
            enemy.ProjectileShooter.PerformSetup(enemy.AttackHandler.GetTotalDamage(this), enemy.AttackHandler, this);
            enemy.ProjectileShooter.ShootProjectile();
        }

        public override bool CheckDealDamage(Enemy enemy)
        {
            return true;
        }
    }
}