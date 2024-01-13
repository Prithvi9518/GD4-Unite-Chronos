using System.Collections;
using Unite.Core;
using Unite.Core.DamageInterfaces;
using Unite.Core.Game;
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
        private GameStateEvent onPlayerDied;
        
        private Health playerHealth;

        private bool regenEnabled;
        private float regenerationIntervalInSeconds;
        private float regenerationPercent;
        private Coroutine regenerationCoroutine;
        
        private void Awake()
        {
            playerHealth = GetComponent<Health>();
        }

        public void PerformSetup(float baseHealth)
        {
            playerHealth.MaxHealth = baseHealth;
            playerHealth.ResetHealth();
        }

        public void TakeDamage(float damage)
        {
            playerHealth.DecreaseHealth(damage);
            
            onHealthChanged.Raise(new HealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));

            if (!(playerHealth.CurrentHealth <= 0)) return;
            StopRegeneration();
            Die();
        }
        
        public void StartRegeneration(float regenPercentage, float intervalInSeconds)
        {
            regenEnabled = true;
            regenerationPercent = regenPercentage;
            regenerationIntervalInSeconds = intervalInSeconds;
            regenerationCoroutine = StartCoroutine(RegenerationCoroutine());
        }
        
        private void Die()
        {
            onPlayerDied.Raise(GameState.PlayerDead);
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