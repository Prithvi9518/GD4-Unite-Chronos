using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Unite
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyDamager))]
    [RequireComponent(typeof(EnemyStateMachine))]
    [RequireComponent(typeof(EnemyAttackHandler))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyDetectionHandler))]
    [RequireComponent(typeof(EnemyAnimationHandler))]
    public class BaseEnemySetup : MonoBehaviour, ISetupEnemy
    {
        [SerializeField]
        private EnemyData enemyData;

        private Health enemyHealth;
        private EnemyStateMachine enemyStateMachine;
        private EnemyAttackHandler enemyAttackHandler;
        private EnemyDetectionHandler enemyDetectionHandler;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
            enemyStateMachine = GetComponent<EnemyStateMachine>();
            enemyAttackHandler = GetComponent<EnemyAttackHandler>();
            enemyDetectionHandler = GetComponent<EnemyDetectionHandler>();
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
    }
}