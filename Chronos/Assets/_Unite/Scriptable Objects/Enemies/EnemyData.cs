using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Unite/Scriptable Objects/Enemies/Base Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Stats")]
        [SerializeField]
        private float baseHealth;

        [SerializeField]
        private float baseDamage;

        [Header("Attack Configuration")]
        [SerializeField]
        private List<AttackData> attacks;

        private float damage;

        [Header("State Machine Configuration")]
        [SerializeField]
        private State startingState;

        [SerializeField]
        private State remainState;

        public State StartState => startingState;
        public State RemainState => remainState;

        private void OnEnable()
        {
            damage = baseDamage;
        }

        private void OnDisable()
        {
            damage = baseDamage;
        }

        public virtual void SetupEnemy(ISetupEnemy enemy)
        {
            enemy.SetupHealth(baseHealth);
            enemy.SetupBaseDamage(baseDamage);
            enemy.SetupAttacks(attacks);
            enemy.SetupStateMachine(this);
        }
    }
}