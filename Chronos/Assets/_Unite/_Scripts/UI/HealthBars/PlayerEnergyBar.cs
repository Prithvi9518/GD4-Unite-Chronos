using Unite.AbilitySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class PlayerEnergyBar : MonoBehaviour
    {
        [SerializeField] private Image imageSlider;

        [SerializeField]
        private Color readyColor;

        [SerializeField]
        private Color activeColor;
        
        [SerializeField]
        private Color cooldownColor;
        
        private Ability ability;

        private void Update()
        {
            // Check if the ability is null or in the ready state
            if (ability == null) return;
            
            if (ability.CurrentState == AbilityState.Ready)
            {
                if (imageSlider.color != readyColor)
                {
                    imageSlider.color = readyColor;
                }

                return;
            }
            
            // If the ability is active or on cooldown, update the energy bar
            UpdateEnergyBar();
        }

        public void ListenToPlayerReadyEvent(Player.Player player)
        { 
            ability = player.Ability;
        }

        // Update the energy bar based on the current state of the ability
        private void UpdateEnergyBar()
        {
            if (ability.CurrentState == AbilityState.Active)
            {
                if (imageSlider.color != activeColor)
                {
                    imageSlider.color = activeColor;
                }

                imageSlider.fillAmount = ability.RemainingActiveTimeMs / ability.Data.ActiveTimeMs();
            }
            else if (ability.CurrentState == AbilityState.Cooldown)
            {
                if (imageSlider.color != cooldownColor)
                {
                    imageSlider.color = cooldownColor;
                }
                
                imageSlider.fillAmount = 1 - (ability.RemainingCooldownTimeMs / ability.Data.CooldownTimeMs());
            }

        }
    }
}