using UnityEngine;

namespace Unite.Enemies.Projectiles
{
    public class DefaultProjectileShooter : MonoBehaviour, IShootProjectile
    {
        [SerializeField]
        private Projectile projectilePrefab;

        [SerializeField]
        private Transform projectileSpawnPoint;

        private EnemyAttackHandler attackHandler;
        private Attack attack;

        public void ShootProjectile()
        {
            Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            projectile.PerformSetup(attackHandler, attack);
        }

        public void PerformSetup(EnemyAttackHandler handler, AttackName attackName)
        {
            attackHandler = handler;
            attack = attackHandler.Attacks[attackName];
        }
    }
}