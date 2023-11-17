using UnityEngine;

namespace Unite.AbilitySystem
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] private AbilityData abilityData;

        private AbilityState currentState;
        private float remainingActiveTimeMs;
        private float remainingCooldownTimeMs;

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

    }
}

