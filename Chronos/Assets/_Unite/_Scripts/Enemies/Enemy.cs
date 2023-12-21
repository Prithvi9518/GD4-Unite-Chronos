using Unite.Core;
using Unite.Enemies.AI;
using Unite.EventSystem;
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
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private GameEvent onEnemyDead;
        
        private Health enemyHealth;
        private EnemyDamager enemyDamager;

        private EnemyStateMachine enemyStateMachine;
        private NavMeshAgent navMeshAgent;

        private Collider collider;

        private EnemyAttackHandler enemyAttackHandler;
        private EnemyDetectionHandler enemyDetectionHandler;
        private EnemyAnimationHandler enemyAnimationHandler;
        private EnemyUIHandler enemyUIHandler;

        private IObjectPool<Enemy> enemyPool;

        private bool isAlive;

        public Health Health => enemyHealth;
        public NavMeshAgent Agent => navMeshAgent;
        public EnemyStateMachine StateMachine => enemyStateMachine;
        public Collider Collider => collider;
        public EnemyDetectionHandler DetectionHandler => enemyDetectionHandler;
        public EnemyAttackHandler AttackHandler => enemyAttackHandler;
        public EnemyAnimationHandler AnimationHandler => enemyAnimationHandler;
        public EnemyUIHandler UIHandler => enemyUIHandler;
        public EnemyDamager Damager => enemyDamager;

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

            isAlive = true;
        }

        public void SetEnemyPool(IObjectPool<Enemy> pool)
        {
            enemyPool = pool;
        }

        public void OnGetFromPool(Transform target)
        {
            enemyDetectionHandler.Target = target;
            navMeshAgent.enabled = true;
            collider.enabled = true;

            enemyAnimationHandler.Animator.enabled = true;

            enemyHealth.ResetHealth();
            
            isAlive = true;
        }

        public void OnEnemyDeath()
        {
            onEnemyDead.Raise();
            isAlive = false;
            gameObject.SetActive(false);
            enemyPool?.Release(this);
        }
    }
}