using UnityEngine;

namespace Unite
{
    [RequireComponent(typeof(Health))]
    public class PlayerDamager : MonoBehaviour, ITakeDamage
    {
        private Health playerHealth;

        private void Awake()
        {
            playerHealth = GetComponent<Health>();
        }

        public void TakeDamage(float damage)
        {
            playerHealth.DecreaseHealth(damage);
            if (playerHealth.CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Debug.Log("Player Dead");
        }
    }
}