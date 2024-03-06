using Unite.Core;
using Unite.Detection;
using Unite.Enemies.AI;
using Unite.Enemies.Movement;
using Unite.EventSystem;
using Unite.ItemDropSystem;
using Unite.Managers;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Unite.Enemies
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyDamager))]
    [RequireComponent(typeof(EnemyStateMachine))]
    [RequireComponent(typeof(EnemyAttackHandler))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyDetectionHandler))]
    [RequireComponent(typeof(EnemyAnimationHandler))]
    [RequireComponent(typeof(EnemyUIHandler))]
    [RequireComponent(typeof(EnemyDropHandler))]
    [RequireComponent(typeof(LineOfSightDetection))]
    [RequireComponent(typeof(TimeStopEnemy))]
    [RequireComponent(typeof(EnemyStatusEffectable))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private GameEvent onEnemyDead;

        [SerializeField]
        private EnemyEvent onEnemyDeadUpdateMetric;

        private Health enemyHealth;
        private EnemyDamager enemyDamager;

        private EnemyStateMachine enemyStateMachine;
        private NavMeshAgent navMeshAgent;

        private Collider collider;

        private EnemyAttackHandler enemyAttackHandler;
        private EnemyDetectionHandler enemyDetectionHandler;
        private EnemyAnimationHandler enemyAnimationHandler;
        private EnemyUIHandler enemyUIHandler;
        private EnemyDropHandler dropHandler;

        private StrafeHandler strafeHandler;

        private EnemyProjectileShooter projectileShooter;

        private IObjectPool<Enemy> enemyPool;

        private bool isAlive;

        public string DisplayName { get; set; }
        
        public Health Health => enemyHealth;
        public NavMeshAgent Agent => navMeshAgent;
        public EnemyStateMachine StateMachine => enemyStateMachine;
        public Collider Collider => collider;
        public EnemyDetectionHandler DetectionHandler => enemyDetectionHandler;
        public EnemyAttackHandler AttackHandler => enemyAttackHandler;
        public EnemyAnimationHandler AnimationHandler => enemyAnimationHandler;
        public EnemyUIHandler UIHandler => enemyUIHandler;
        public EnemyDamager Damager => enemyDamager;
        public StrafeHandler StrafeHandler => strafeHandler;
        public EnemyProjectileShooter ProjectileShooter => projectileShooter;

        public bool IsAlive => isAlive;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
            enemyDamager = GetComponent<EnemyDamager>();

            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyStateMachine = GetComponent<EnemyStateMachine>();

            collider = GetComponent<Collider>();

            enemyAttackHandler = GetComponent<EnemyAttackHandler>();
            enemyDetectionHandler = GetComponent<EnemyDetectionHandler>();
            enemyAnimationHandler = GetComponent<EnemyAnimationHandler>();
            enemyUIHandler = GetComponent<EnemyUIHandler>();
            dropHandler = GetComponent<EnemyDropHandler>();

            strafeHandler = GetComponent<StrafeHandler>();
            
            projectileShooter = GetComponent<EnemyProjectileShooter>();

            isAlive = true;
        }
        
        private void OnEnable()
        {
            GameManager.Instance.OnGameRestart += DestroySelf;
            GameManager.Instance.OnGameLose += DestroySelf;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameRestart -= DestroySelf;
            GameManager.Instance.OnGameLose -= DestroySelf;
        }

        public void SetEnemyPool(IObjectPool<Enemy> pool)
        {
            enemyPool = pool;
        }

        public void OnGetFromPool(Transform target)
        {
            enemyDetectionHandler.Target = target;
            collider.enabled = true;

            enemyAnimationHandler.Animator.enabled = true;

            enemyHealth.ResetHealth();
            
            isAlive = true;
        }

        public void OnEnemyDeath()
        {
            dropHandler.DropItems();
            onEnemyDead.Raise();
            onEnemyDeadUpdateMetric.Raise(this);
            isAlive = false;
            navMeshAgent.enabled = false;
            gameObject.SetActive(false);
            enemyPool?.Release(this);
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
            // gameObject.SetActive(false);
            // enemyPool?.Release(this);
        }
    }
}