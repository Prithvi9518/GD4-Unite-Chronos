using System.Collections;
using Unite.Core;
using Unite.Core.DamageInterfaces;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(Health))]
    public class PlayerHealthHandler : MonoBehaviour, ITakeDamage
    {
        [SerializeField]
        private HealthInfoEvent onHealthChanged;
        
        [SerializeField] 
        private PlayerDiedInfoEvent onPlayerDied;
        
        private Health playerHealth;

        private bool regenEnabled;
        private float regenerationIntervalInSeconds;
        private float regenerationPercent;
        private Coroutine regenerationCoroutine;

        private bool dead;
        
        private void Awake()
        {
            playerHealth = GetComponent<Health>();
        }

        public void PerformSetup(float baseHealth)
        {
            dead = false;
            playerHealth.MaxHealth = baseHealth;
            playerHealth.ResetHealth();
        }

        public void AddHealth(float amount)
        {
            playerHealth.AddHealth(amount);
            onHealthChanged.Raise(new HealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));
        }

        public void TakeDamage(float damage, IAttacker attacker, IDoDamage attack)
        {
            if (dead) return;
            
            playerHealth.DecreaseHealth(damage);
            
            onHealthChanged.Raise(new HealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));

            if (playerHealth.CurrentHealth > 0) return;
            
            StopRegeneration();
            Die(attacker, attack);
        }
        
        public void StartRegeneration(float regenPercentage, float intervalInSeconds)
        {
            regenEnabled = true;
            regenerationPercent = regenPercentage;
            regenerationIntervalInSeconds = intervalInSeconds;
            regenerationCoroutine = StartCoroutine(RegenerationCoroutine());
        }
        
        private void Die(IAttacker attacker, IDoDamage attack)
        {
            if (dead) return;
            
            dead = true;
            onPlayerDied.Raise(new PlayerDiedInfo(transform.position, attacker.GetName(), attack.GetName()));
        }

        private void StopRegeneration()
        {
            if (regenerationCoroutine == null) return;
            StopCoroutine(regenerationCoroutine);
        }

        private IEnumerator RegenerationCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(regenerationIntervalInSeconds);
                
                if (playerHealth.IsAtFullHealth()) continue;
                
                float regenAmount = (regenerationPercent * playerHealth.MaxHealth)/100f;
                playerHealth.AddHealth(regenAmount);
                onHealthChanged.Raise(new HealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));
            }
        }
    }
}