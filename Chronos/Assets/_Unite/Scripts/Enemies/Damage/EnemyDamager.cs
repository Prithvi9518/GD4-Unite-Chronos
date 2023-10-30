using UnityEngine;

namespace Unite
{
    [RequireComponent(typeof(Health))]
    public class EnemyDamager : MonoBehaviour, ITakeDamage
    {
        private Health enemyHealth;

        private bool delayDeath;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
        }

        public void TakeDamage(float damage)
        {
            enemyHealth.DecreaseHealth(damage);

            if (enemyHealth.CurrentHealth <= 0 && !delayDeath)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
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