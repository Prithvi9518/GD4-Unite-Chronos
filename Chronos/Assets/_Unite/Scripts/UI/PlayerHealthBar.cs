using UnityEngine;
using UnityEngine.UI;

namespace Unite
{
    public class PlayerHealthBar : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            PlayerDamager.OnPlayerDamaged += UpdateHealthBar;
        }

        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            slider.value = currentHealth / maxHealth;
        }
    }
}

