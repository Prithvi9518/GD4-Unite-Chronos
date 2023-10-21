using UnityEngine;

namespace Unite
{
    [RequireComponent(typeof(Health))]
    public class EnemyDamager : MonoBehaviour, ITakeDamage
    {
        private Health enemyHealth;

        private void Awake()
        {
            enemyHealth = GetComponent<Health>();
        }

        public void TakeDamage(float damage)
        {
            enemyHealth.DecreaseHealth(damage);

            // do some other logic

            // die
            if (enemyHealth.GetHealth() <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}