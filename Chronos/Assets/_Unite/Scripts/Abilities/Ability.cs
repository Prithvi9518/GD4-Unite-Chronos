using UnityEngine;
using UnityEngine.InputSystem;

namespace Unite
{
    public class Ability : MonoBehaviour
    {
        [SerializeField] private AbilityData abilityData;
        [SerializeField] private InputActionReference inputAction;

        private AbilityState currentState;
        private float remainingActiveTimeMs;
        private float remainingCooldownTimeMs;

        private void OnEnable()
        {
            inputAction.action.performed += ProcessActivation;
            inputAction.action.Enable();
        }

        private void OnDisable()
        {
            inputAction.action.performed -= ProcessActivation;
            inputAction.action.Disable();
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

        private void ProcessActivation(InputAction.CallbackContext ctx)
        {
            if (currentState != AbilityState.Ready) return;

            if (inputAction.action.WasPressedThisFrame())
            {
                abilityData.Activate();
                remainingActiveTimeMs = abilityData.ActiveTimeMs();
                currentState = AbilityState.Active;
            }
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

