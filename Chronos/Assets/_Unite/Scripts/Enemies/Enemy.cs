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
    public class Enemy : MonoBehaviour
    {
        private Health enemyHealth;
        private EnemyDamager enemyDamager;

        private EnemyStateMachine enemyStateMachine;
        private NavMeshAgent navMeshAgent;

        private EnemyAttackHandler enemyAttackHandler;
        private EnemyDetectionHandler enemyDetectionHandler;
        private EnemyAnimationHandler enemyAnimationHandler;

        private IObjectPool<Enemy> enemyPool;

        public Health Health => enemyHealth;
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

        public void SetEnemyPool(IObjectPool<Enemy> pool)
        {
            enemyPool = pool;
        }

        public void OnGetFromPool(Transform target)
        {
            enemyDetectionHandler.Target = target;
            navMeshAgent.enabled = true;

            enemyAnimationHandler.Animator.enabled = true;

            enemyHealth.ResetHealth();
        }

        public void OnEnemyDeath()
        {
            enemyPool.Release(this);
        }
    }
}