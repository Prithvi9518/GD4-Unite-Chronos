using Unite.AbilitySystem;
using Unite.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Unite.UI
{
    public class PlayerEnergyBar : MonoBehaviour
    {
        [SerializeField] private Image imageSlider;
        private Ability ability;

        private void Update()
        {
            // Check if the ability is null or in the ready state
            if (ability == null) return;
            if (ability.CurrentState == AbilityState.Ready) return;
            // If the ability is active or on cooldown, update the energy bar
            UpdateEnergyBar();
        }

        public void ListenToAbilityEvent(Ability ability)
        {
            this.ability = ability;
        }

        // Update the energy bar based on the current state of the ability
        public void UpdateEnergyBar()
        {
            float fillAmountDelta = Time.deltaTime / 1000f;
            if (ability.CurrentState == AbilityState.Active)
            {
                // If the ability is active, decrease the fillAmount over time based on the remaining active time
                imageSlider.fillAmount -= fillAmountDelta / ability.RemainingActiveTimeMs;
                imageSlider.fillAmount = Mathf.Clamp01(imageSlider.fillAmount);
            }
            else if (ability.CurrentState == AbilityState.Cooldown)
            {
                // If the ability is on cooldown, start a coroutine to handle the update over time during the cooldown
                StartCoroutine(UpdateEnergyBarRoutine());
            }

        }

        // Coroutine to update the energy bar during the cooldown with a delay
        IEnumerator UpdateEnergyBarRoutine()
        {
            float fillAmountDelta = Time.deltaTime / 1000f;

            yield return new WaitForSeconds(5f);
            // After the delay, increase the fillAmount over time based on theremaining cooldown time

            imageSlider.fillAmount += fillAmountDelta / ability.RemainingCooldownTimeMs;
            imageSlider.fillAmount = Mathf.Clamp01(imageSlider.fillAmount);
        }
    }
}