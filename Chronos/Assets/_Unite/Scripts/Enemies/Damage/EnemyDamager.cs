using UnityEngine;

namespace Unite
{
    [RequireComponent(typeof(Health))]
    public class EnemyDamager : MonoBehaviour, ITakeDamage
    {
        private Health enemyHealth;
        private Enemy enemy;

        private bool delayDeath;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
            enemy = GetComponent<Enemy>();
        }

        public void TakeDamage(float damage)
        {
            enemyHealth.DecreaseHealth(damage);
            enemy.UIHandler.UpdateHealthBar(enemyHealth.CurrentHealth, enemyHealth.MaxHealth);
            enemy.StateMachine.TriggerStateEvent(StateEvent.EnemyTookDamage);
            
            if (enemyHealth.CurrentHealth <= 0 && !delayDeath)
            {
                Die();
            }
        }

        private void Die()
        {
            enemy.OnEnemyDeath();
        }

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