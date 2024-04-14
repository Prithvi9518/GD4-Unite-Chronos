using Unite.Core;
using Unite.Core.DamageInterfaces;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies
{
    /// <summary>
    /// Handles enemy health and damage
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class EnemyDamager : MonoBehaviour, ITakeDamage
    {
        private Health enemyHealth;
        private Enemy enemy;

        private bool delayDeath;

        public bool DelayDeath => delayDeath;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
            enemy = GetComponent<Enemy>();
        }

        public void TakeDamage(float damage, IAttacker attacker, IDoDamage attack)
        {
            enemyHealth.DecreaseHealth(damage);
            enemy.UIHandler.UpdateHealthBar(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
            enemy.StateMachine.TriggerStateEvent(StateEvent.EnemyTookDamage);

            if (enemyHealth.CurrentHealth <= 0)
            {
                enemy.UIHandler.ToggleHealthBar(false);
            }
            
            if (enemyHealth.CurrentHealth <= 0 && !delayDeath)
            {
                Die();
            }
        }

        private void Die()
        {
            enemy.StateMachine.TriggerStateEvent(StateEvent.EnemyDead);
        }

        /// <summary>
        /// Used to delay death until after the time-stop is finished.
        /// </summary>
        public void ToggleDelayDeath(bool value)
        {
            delayDeath = value;
            if (!delayDeath)
            {
                CheckDiedAfterDelay();
            }
        }

        private void CheckDiedAfterDelay()
        {
            if (enemyHealth.CurrentHealth <= 0)
            {
                Die();
            }
        }
    }
}