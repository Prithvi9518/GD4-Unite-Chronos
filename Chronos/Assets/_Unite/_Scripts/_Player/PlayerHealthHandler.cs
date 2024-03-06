using System.Collections;
using Unite.Core;
using Unite.Core.DamageInterfaces;
using Unite.EventSystem;
using Unite.SoundScripts;
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

        [SerializeField]
        private TransformEvent onPlayerHitSendInfo;

        [Header("Damage SFX Configuration")]
        [SerializeField]
        private AudioClip damageAudioClip;

        [SerializeField]
        [Range(0,1)]
        private float volume = 1f;

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
        }

        public void PerformSetup(PlayerStatsHandler playerStatsHandler)
        {
            dead = false;
            regenEnabled = false;

            statsHandler = playerStatsHandler;
            
            playerHealth.MaxHealth = statsHandler.GetStat(healthStatType).Value;
            playerHealth.ResetHealth();
            
            onHealthChanged.Raise(new HealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));
        }

        public void UpdateMaxHealthFromStats()
        {
            playerHealth.MaxHealth = statsHandler.GetStat(healthStatType).Value;
            playerHealth.ResetHealth();
            onHealthChanged.Raise(new HealthInfo(playerHealth.CurrentHealth, playerHealth.MaxHealth));
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
            TrySendAttackerTransformEvent(attacker, attack);
            
            SoundEffectsManager.Instance.PlaySoundAtCameraPosition(damageAudioClip, volume);

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

        private void TrySendAttackerTransformEvent(IAttacker attacker, IDoDamage attack)
        {
            DamageType damageType = attack.GetDamageType();
            if (damageType != DamageType.Direct) return;
            
            onPlayerHitSendInfo.Raise(attacker.GetTransform());
        }
    }
}