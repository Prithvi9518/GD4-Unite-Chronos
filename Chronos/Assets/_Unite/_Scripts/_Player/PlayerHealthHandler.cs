using System.Collections;
using Unite.Core;
using Unite.Core.DamageInterfaces;
using Unite.EventSystem;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    [RequireComponent(typeof(Health))]
    public class PlayerHealthHandler : MonoBehaviour, ITakeDamage
    {
        [SerializeField]
        private StatTypeSO healthStatType;
        
        [SerializeField]
        private HealthInfoEvent onHealthChanged;
        
        [SerializeField] 
        private PlayerDiedInfoEvent onPlayerDied;
        
        private Health playerHealth;
        private PlayerStatsHandler statsHandler;

        private bool regenEnabled;
        private float regenerationIntervalInSeconds;
        private float regenerationPercent;
        private Coroutine regenerationCoroutine;

        private bool dead;
        
        private void Awake()
        {
            playerHealth = GetComponent<Health>();
            statsHandler = GetComponent<PlayerStatsHandler>();
        }

        public void PerformSetup()
        {
            dead = false;
            regenEnabled = false;
            
            playerHealth.MaxHealth = statsHandler.GetStat(healthStatType).Value;
            playerHealth.ResetHealth();
        }

        public void UpdateMaxHealthFromStats()
        {
            Debug.Log($"Old max health = {playerHealth.MaxHealth}");
            playerHealth.MaxHealth = statsHandler.GetStat(healthStatType).Value;
            playerHealth.ResetHealth();
            onHealthChanged.Raise(new HealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));
            Debug.Log($"New max health = {playerHealth.MaxHealth}");
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

            CheckHitDirection(attacker, attack);

            if (playerHealth.CurrentHealth > 0) return;
            
            StopRegeneration();
            Die(attacker, attack);
        }
        
        public void ApplyRegeneration(float regenPercentage, float intervalInSeconds)
        {
            if (!regenEnabled)
            {
                regenEnabled = true;
                regenerationPercent = regenPercentage;
                regenerationIntervalInSeconds = intervalInSeconds;
                regenerationCoroutine = StartCoroutine(RegenerationCoroutine());
            }
            else
            {
                regenerationPercent += regenPercentage;
            }
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

        private void CheckHitDirection(IAttacker attacker, IDoDamage attack)
        {
            DamageType damageType = attack.GetDamageType();
            if (damageType != DamageType.Direct) return;
            
            Transform attackerTransform = attacker.GetTransform();
            Vector3 hitDirection = transform.position - attackerTransform.position;
                
            Debug.Log($"hitDirection = {hitDirection}");
        }
    }
}