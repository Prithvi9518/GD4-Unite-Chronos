using System.Collections.Generic;
using Unite.Core.DamageInterfaces;
using Unite.StatusEffectSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerStatusEffectable : MonoBehaviour, IStatusEffectable
    {
        private class StatusEffectInfo
        {
            public StatusEffectSO effectData;
            public IAttacker effectUser;
            public float timeSinceEffectApplied;
            public float nextEffectUpdateTime;

            public StatusEffectInfo(StatusEffectSO se, IAttacker attacker)
            {
                effectData = se;
                effectUser = attacker;
                timeSinceEffectApplied = 0;
                nextEffectUpdateTime = 0;
            }
        }
        
        private PlayerHealthHandler healthHandler;
        private Dictionary<StatusEffectSO, StatusEffectInfo> effectsDict = new();
        
        private void Update()
        {
            if (effectsDict.Count == 0) return;
            HandleEffect();
        }
        
        public void ApplyStatusEffect(StatusEffectSO statusEffect, IAttacker attacker)
        {
            StatusEffectInfo info = new StatusEffectInfo(statusEffect, attacker);
            
            if (effectsDict.ContainsKey(statusEffect))
                effectsDict.Remove(statusEffect);
            
            effectsDict.Add(statusEffect, info);
        }

        public void RemoveEffect()
        {
        }

        public void HandleEffect()
        {
            List<StatusEffectInfo> values = new List<StatusEffectInfo>(effectsDict.Values);
            for (int i = 0; i < values.Count; i++)
            {
                StatusEffectInfo info = values[i];
                
                info.timeSinceEffectApplied += Time.deltaTime;
                if (info.timeSinceEffectApplied >= info.effectData.LifetimeInSeconds)
                {
                    effectsDict.Remove(info.effectData);
                    continue;
                }
                
                if(info.timeSinceEffectApplied < info.nextEffectUpdateTime) continue;
                info.nextEffectUpdateTime += info.effectData.IntervalInSeconds;
                
                if(info.effectData.DamageOverTime == 0) continue;
                healthHandler.TakeDamage(info.effectData.DamageOverTime, info.effectUser, info.effectData);
                
                if(info.effectData.SlowdownPenalty == 0) continue;
            }
        }

        public void PerformSetup(Player p)
        {
            healthHandler = p.HealthHandler;
        }
    }
}