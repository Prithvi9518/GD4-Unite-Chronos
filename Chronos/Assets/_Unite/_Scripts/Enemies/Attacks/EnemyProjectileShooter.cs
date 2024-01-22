using System.Collections.Generic;
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
        private Transform projectileSpawnPoint;
        
        [Header("Charge Settings")]
        [SerializeField]
        private bool hasChargeTime;

        [SerializeField]
        private float chargeTimeInSeconds;

        [SerializeField]
        private List<Projectile> allProjectiles;

        private Projectile projectilePrefab;
        
        private Dictionary<string, ObjectPool<Projectile>> pools = new();

        private EnemyDetectionHandler detectionHandler;
        private EnemyAttackHandler attackHandler;
        private AttackData attack;
        private float damage;

        private bool isCharging;
        private float chargeTimer;

        private void Awake()
        {
            SetupPools();

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

        private void SetupPools()
        {
            foreach (var projectile in allProjectiles)
            {
                ObjectPool<Projectile> projectilePool = new ObjectPool<Projectile>(
                    () => CreateProjectile(projectile),
                    actionOnGet:GetProjectileFromPool);
                pools.Add(projectile.ID, projectilePool);
            }
        }

        private Projectile CreateProjectile(Projectile prefab)
        {
            return Instantiate(prefab, projectileSpawnPoint.position, Quaternion.identity);
        }
        
        private void GetProjectileFromPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
            projectile.transform.SetPositionAndRotation(projectileSpawnPoint.position, transform.rotation);
            projectile.PerformSetup(damage, pools[projectile.ID], attackHandler, attack, detectionHandler.Target);
        }

        private void SpawnProjectile()
        {
            if (projectilePrefab == null) return;
            Projectile projectile = pools[projectilePrefab.ID].Get();
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

        public void PerformSetup(float damageAmount, EnemyAttackHandler enemyAttackHandler, ProjectileAttack projectileAttack)
        {
            attackHandler = enemyAttackHandler;
            attack = projectileAttack;
            damage = damageAmount;
            projectilePrefab = projectileAttack.ProjectilePrefab;
            isCharging = false;
            chargeTimer = 0;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            isCharging = !isTimeStopped;
        }
    }
}