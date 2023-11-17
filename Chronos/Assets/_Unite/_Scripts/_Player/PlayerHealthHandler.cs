using Unite.Core;
using Unite.Core.DamageInterfaces;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(Health))]
    public class PlayerHealthHandler : MonoBehaviour, ITakeDamage
    {
        private Health playerHealth;

        [SerializeField]
        private HealthInfoEvent onDamaged;
        
        private void Awake()
        {
            playerHealth = GetComponent<Health>();
        }

        public void TakeDamage(float damage)
        {
            playerHealth.DecreaseHealth(damage);
            
            onDamaged.Raise(new HealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));

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