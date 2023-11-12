using UnityEngine;

namespace Unite
{
    [RequireComponent(typeof(Health))]
    public class PlayerHealthHandler : MonoBehaviour, ITakeDamage
    {
        private Health playerHealth;

        public delegate void HandlePlayerDamaged(float currentHealth, float maxHealth);

        public static event HandlePlayerDamaged OnPlayerDamaged;

        private void Awake()
        {
            playerHealth = GetComponent<Health>();
        }

        public void TakeDamage(float damage)
        {
            playerHealth.DecreaseHealth(damage);
            
            OnPlayerDamaged?.Invoke(playerHealth.CurrentHealth, playerHealth.MaxHealth);

            if (playerHealth.CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
        }
    }
}