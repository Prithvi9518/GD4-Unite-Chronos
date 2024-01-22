using Unite.Enemies.AI;
using Unite.Projectiles;
using Unite.TimeStop;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Enemies
{
    public class EnemyProjectileShooter : MonoBehaviour, IShootProjectile, ITimeStopSubscriber
    {
        [SerializeField]
        private Projectile projectilePrefab;

        [SerializeField]
        private Transform projectileSpawnPoint;

        [Header("Charge Settings")]
        [SerializeField]
        private bool hasChargeTime;

        [SerializeField]
        private float chargeTimeInSeconds;

        private ObjectPool<Projectile> projectilePool;

        private EnemyDetectionHandler detectionHandler;
        private EnemyAttackHandler attackHandler;
        private AttackData attack;
        private float damage;

        private bool isCharging;
        private float chargeTimer;

        private void Awake()
        {
            projectilePool = new ObjectPool<Projectile>(CreateProjectile,
                actionOnGet:GetProjectileFromPool);

            detectionHandler = GetComponent<EnemyDetectionHandler>();
        }

        private void Update()
        {
            if (!hasChargeTime) return;
            if (!isCharging) return;

            if (chargeTimer >= chargeTimeInSeconds)
            {
                SpawnProjectile();
                chargeTimer = 0;
                isCharging = false;
            }
            else
            {
                chargeTimer += Time.deltaTime;
            }
        }

        private Projectile CreateProjectile()
        {
            return Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        }
        
        private void GetProjectileFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
            projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position, transform.rotation);
            projectile.PerformSetup(damage, projectilePool, attackHandler, attack, detectionHandler.Target);
        }

        private void SpawnProjectile()
        {
            Projectile projectile = projectilePool.Get();
            projectile.Spawn();
        }
        
        public void ShootProjectile()
        {
            if (hasChargeTime)
            {
                isCharging = true;
            }
            else
            { 
                SpawnProjectile();
            }
        }

        public void PerformSetup(float damageAmount, EnemyAttackHandler enemyAttackHandler, AttackData projectileAttack)
        {
            attackHandler = enemyAttackHandler;
            attack = projectileAttack;
            damage = damageAmount;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            isCharging = !isTimeStopped;
        }
    }
}