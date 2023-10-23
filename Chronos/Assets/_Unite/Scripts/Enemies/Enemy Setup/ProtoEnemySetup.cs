using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    public class ProtoEnemySetup : MonoBehaviour, ISetupEnemy
    {
        [SerializeField]
        private EnemyData enemyData;

        private Health enemyHealth;
        private EnemyStateMachine enemyStateMachine;
        private EnemyAttackHandler enemyAttackHandler;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
            enemyStateMachine = GetComponent<EnemyStateMachine>();
            enemyAttackHandler = GetComponent<EnemyAttackHandler>();
        }

        private void Start()
        {
            enemyData.SetupEnemy(this);
        }

        public void SetupAttacks(List<AttackData> attacks)
        {
            enemyAttackHandler.SetupAttackDict(attacks);
        }

        public void SetupBaseDamage(float baseDamage)
        {
        }

        public void SetupHealth(float maxHealth)
        {
            enemyHealth.MaxHealth = maxHealth;
            enemyHealth.ResetHealth();
        }

        public void SetupStateMachine(EnemyData enemyData)
        {
            enemyStateMachine.SetupStates(enemyData);
        }
    }
}