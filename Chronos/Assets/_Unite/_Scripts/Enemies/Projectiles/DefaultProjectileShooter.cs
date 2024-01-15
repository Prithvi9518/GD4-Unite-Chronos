using Unite.Enemies.AI;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Enemies.Projectiles
{
    public class DefaultProjectileShooter : MonoBehaviour, IShootProjectile
    {
        [SerializeField]
        private Projectile projectilePrefab;

        [SerializeField]
        private Transform projectileSpawnPoint;

        private ObjectPool<Projectile> projectilePool;

        private EnemyDetectionHandler detectionHandler;
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
            projectile.PerformSetup(damage, projectilePool);
        }

        public void ShootProjectile()
        {
            Projectile projectile = projectilePool.Get();
            Vector3 shootDir = (detectionHandler.Target.position - transform.position).normalized;
            projectile.Rigidbody.AddForce(shootDir * projectile.MoveSpeed, ForceMode.VelocityChange);
        }

        public void PerformSetup(float damageAmount)
        {
            damage = damageAmount;
        }
    }
}