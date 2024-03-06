using System;
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
        private IHandlePlayerMovement movementHandler;

        private bool isActive;

        private Dictionary<StatusEffectSO, StatusEffectInfo> effectsDict = new();

        private void Awake()
        {
            isActive = true;
        }

        private void Update()
        {
            if (!isActive) return;
            if (effectsDict.Count == 0) return;
            HandleEffect();
        }
        
        public void ApplyStatusEffect(StatusEffectSO statusEffect, IAttacker attacker)
        {
            StatusEffectInfo info = new StatusEffectInfo(statusEffect, attacker);

            if (effectsDict.ContainsKey(statusEffect))
            {
                effectsDict.Remove(statusEffect);
                movementHandler.ModifySpeed(info.effectData.SlowdownPenalty * -1);
            }
            
            effectsDict.Add(statusEffect, info);
            movementHandler.ModifySpeed(info.effectData.SlowdownPenalty);
        }

        public void RemoveEffect()
        {
        }

        public void HandleEffect()
        {
            Debug.Log("Handle Effect");
            
            List<StatusEffectInfo> values = new List<StatusEffectInfo>(effectsDict.Values);
            for (int i = 0; i < values.Count; i++)
            {
                StatusEffectInfo info = values[i];
                
                info.timeSinceEffectApplied += Time.deltaTime;
                if (info.timeSinceEffectApplied >= info.effectData.LifetimeInSeconds)
                {
                    effectsDict.Remove(info.effectData);
                    movementHandler.ModifySpeed(info.effectData.SlowdownPenalty * -1);
                    continue;
                }
                
                if(info.timeSinceEffectApplied < info.nextEffectUpdateTime) continue;
                info.nextEffectUpdateTime += info.effectData.IntervalInSeconds;
                
                if(info.effectData.DamageOverTime != 0)
                {
                    healthHandler.TakeDamage(info.effectData.DamageOverTime, info.effectUser, info.effectData);
                }
            }
        }

        public void StopAllEffects()
        {
            isActive = false;
            effectsDict.Clear();
        }

        public void PerformSetup(Player p)
        {
            effectsDict = new();
            healthHandler = p.HealthHandler;
            movementHandler = p.MovementHandler;
            isActive = true;
        }
    }
}