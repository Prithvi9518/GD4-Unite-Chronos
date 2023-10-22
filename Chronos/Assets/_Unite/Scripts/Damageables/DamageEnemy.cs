using UnityEngine;

namespace Unite
{
    [RequireComponent(typeof(Health))]
    public class EnemyDamager : MonoBehaviour, ITakeDamage
    {
        private Health enemyHealth;

        private bool isStoringDamage;
        private float storedDamage;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
        }

        public void TakeDamage(float damage)
        {
            if (isStoringDamage)
            {
                storedDamage += damage;
                return;
            }

            enemyHealth.DecreaseHealth(damage);

            if (enemyHealth.GetHealth() <= 0)
            {
                Die();
            }
        }
        private void Die()
        {
            Destroy(gameObject);
        }

        public void ToggleStoredDamage(bool storeDamage)
        {
            isStoringDamage = storeDamage;
        }

        public void ApplyStoredDamage()
        {
            TakeDamage(storedDamage);
            storedDamage = 0;
        }
    }
}