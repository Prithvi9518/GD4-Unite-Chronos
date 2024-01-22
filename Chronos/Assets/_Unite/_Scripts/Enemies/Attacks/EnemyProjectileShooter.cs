using Unite.Enemies.AI;
using Unite.Projectiles;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Enemies
{
    public class EnemyProjectileShooter : MonoBehaviour, IShootProjectile
    {
        [SerializeField]
        private Projectile projectilePrefab;

        [SerializeField]
        private Transform projectileSpawnPoint;

        private ObjectPool<Projectile> projectilePool;

        private EnemyDetectionHandler detectionHandler;
        private EnemyAttackHandler attackHandler;
        private AttackData attack;
        private float damage;

        private void Awake()
        {
            projectilePool = new ObjectPool<Projectile>(CreateProjectile,
                actionOnGet:GetProjectileFromPool);

            detectionHandler = GetComponent<EnemyDetectionHandler>();
        }

        private Projectile CreateProjectile()
        {
            return Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        }
        
        private void GetProjectileFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
            projectile.transform.position = projectileSpawnPoint.position;
            projectile.transform.rotation = transform.rotation;
            projectile.PerformSetup(damage, projectilePool, attackHandler, attack, detectionHandler.Target);
        }

        public void ShootProjectile()
        {
            Projectile projectile = projectilePool.Get();
            projectile.Spawn();
        }

        public void PerformSetup(float damageAmount, EnemyAttackHandler enemyAttackHandler, AttackData projectileAttack)
        {
            attackHandler = enemyAttackHandler;
            attack = projectileAttack;
            damage = damageAmount;
        }
    }
}