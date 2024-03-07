using System;
using Unite.EventSystem;
using Unite.Managers;
using UnityEngine;

namespace Unite.AbilitySystem
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] private AbilityData abilityData;
        [SerializeField] private AbilityEvent onAbilityInstantiate;

        private AbilityState currentState;
        private float remainingActiveTimeMs;
        private float remainingCooldownTimeMs;

        public AbilityData Data => abilityData;
        public AbilityState CurrentState => currentState;
        public float RemainingActiveTimeMs => remainingActiveTimeMs;
        public float RemainingCooldownTimeMs => remainingCooldownTimeMs;

        private void Start()
        {
            onAbilityInstantiate.Raise(this);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameRestart += ResetAbility;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameRestart -= ResetAbility;
        }

        private void Update()
        {
            switch (currentState)
            {
                case AbilityState.Active:
                    ProcessActiveState();
                    break;

                case AbilityState.Cooldown:
                    ProcessCooldownState();
                    break;

                default:
                    break;
            }
        }

        public void ProcessActivation()
        {
            if (currentState != AbilityState.Ready) return;
            
            abilityData.Activate();
            remainingActiveTimeMs = abilityData.ActiveTimeMs();
            currentState = AbilityState.Active;
        }

        private void ProcessActiveState()
        {
            if (remainingActiveTimeMs > 0)
            {
                remainingActiveTimeMs -= Time.deltaTime;
            }
            else
            {
                abilityData.Deactivate();
                remainingCooldownTimeMs = abilityData.CooldownTimeMs();
                currentState = AbilityState.Cooldown;
            }
        }

        private void ProcessCooldownState()
        {
            if (remainingCooldownTimeMs > 0)
            {
                remainingCooldownTimeMs -= Time.deltaTime;
            }
            else
            {
                currentState = AbilityState.Ready;
                remainingActiveTimeMs = 0;
                remainingCooldownTimeMs = 0;
            }
        }

        private void ResetAbility()
        {
            currentState = AbilityState.Ready;
            remainingActiveTimeMs = 0;
            remainingCooldownTimeMs = 0;
        }

    }
}

