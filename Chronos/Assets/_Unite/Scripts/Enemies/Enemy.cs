using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

namespace Unite
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyDamager))]
    [RequireComponent(typeof(EnemyStateMachine))]
    [RequireComponent(typeof(EnemyAttackHandler))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyDetectionHandler))]
    [RequireComponent(typeof(EnemyAnimationHandler))]
    public class Enemy : MonoBehaviour, ISetupEnemy
    {
        [SerializeField]
        private EnemyData enemyData;

        private Health enemyHealth;
        private EnemyDamager enemyDamager;

        private EnemyStateMachine enemyStateMachine;
        private NavMeshAgent navMeshAgent;

        private EnemyAttackHandler enemyAttackHandler;
        private EnemyDetectionHandler enemyDetectionHandler;
        private EnemyAnimationHandler enemyAnimationHandler;

        private IObjectPool<Enemy> enemyPool;

        public NavMeshAgent Agent => navMeshAgent;
        public EnemyStateMachine StateMachine => enemyStateMachine;
        public EnemyDetectionHandler DetectionHandler => enemyDetectionHandler;
        public EnemyAttackHandler AttackHandler => enemyAttackHandler;
        public EnemyAnimationHandler AnimationHandler => enemyAnimationHandler;
        public EnemyDamager Damager => enemyDamager;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
            enemyDamager = GetComponent<EnemyDamager>();

            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyStateMachine = GetComponent<EnemyStateMachine>();

            enemyAttackHandler = GetComponent<EnemyAttackHandler>();
            enemyDetectionHandler = GetComponent<EnemyDetectionHandler>();
            enemyAnimationHandler = GetComponent<EnemyAnimationHandler>();
        }

        private void Start()
        {
            enemyData.SetupEnemy(this);
        }

        public void SetupAttacks(float baseDamage, List<AttackData> attacks)
        {
            enemyAttackHandler.PerformSetup(baseDamage, attacks);
        }

        public void SetupHealth(float maxHealth)
        {
            enemyHealth.MaxHealth = maxHealth;
            enemyHealth.ResetHealth();
        }

        public void SetupStateMachine(EnemyData enemyData)
        {
            enemyStateMachine.PerformSetup(enemyData);
        }

        public void SetTarget(Transform target)
        {
            enemyDetectionHandler.Target = target;
        }

        public void SetEnemyPool(IObjectPool<Enemy> pool)
        {
            enemyPool = pool;
        }

        public void OnGetFromPool(Transform target)
        {
            enemyDetectionHandler.Target = target;
            navMeshAgent.enabled = true;

            enemyAnimationHandler.Animator.enabled = true;

            enemyStateMachine.enabled = true;
            enemyStateMachine.PerformSetup(enemyData);

            enemyHealth.ResetHealth();
        }

        public void OnEnemyDeath()
        {
            enemyPool.Release(this);
        }
    }
}

