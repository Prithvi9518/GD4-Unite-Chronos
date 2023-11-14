using Unite.Core.DamageInterfaces;
using Unite.Core.EventSystem;
using UnityEngine;

namespace Unite.Core.Player
{
    [RequireComponent(typeof(Health))]
    public class PlayerHealthHandler : MonoBehaviour, ITakeDamage
    {
        private Health playerHealth;

        [SerializeField]
        private PlayerHealthInfoEvent onPlayerDamaged;
        
        private void Awake()
        {
            playerHealth = GetComponent<Health>();
        }

        public void TakeDamage(float damage)
        {
            playerHealth.DecreaseHealth(damage);
            
            onPlayerDamaged.Raise(new PlayerHealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));

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