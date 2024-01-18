using Unite.StatePattern;
using System.Collections.Generic;
using UnityEngine;

namespace Unite.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/Base Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField]
        private string displayName;
        
        [SerializeField]
        private Enemy enemyPrefab;

        [Header("Stats")]
        [SerializeField]
        private float baseHealth;

        [SerializeField]
        private float baseDamage;

        [Header("Spawn Configuration")]
        [SerializeField]
        private int cost;

        [Header("Attack Configuration")]
        [SerializeField]
        private List<AttackData> attacks;

        [Header("State Machine Configuration")]
        [SerializeField]
        private State startingState;

        [SerializeField]
        private State remainState;

        public string DisplayName => displayName;
        public Enemy EnemyPrefab => enemyPrefab;
        public int Cost => cost;

        public virtual void SetupEnemy(Enemy enemy, Transform target)
        {
            enemy.DisplayName = displayName;
            enemy.Health.MaxHealth = baseHealth;
            enemy.Health.ResetHealth();
            
            enemy.UIHandler.UpdateHealthBar(enemy.Health.CurrentHealth, enemy.Health.MaxHealth);

            enemy.AttackHandler.PerformSetup(baseDamage, attacks);
            enemy.DetectionHandler.Target = target;
            enemy.StateMachine.PerformSetup(startingState, remainState);
        }
    }
}