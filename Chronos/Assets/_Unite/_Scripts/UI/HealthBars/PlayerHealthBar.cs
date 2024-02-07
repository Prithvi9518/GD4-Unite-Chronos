using Unite.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private Image imageSlider;

        public void UpdateHealthBar(HealthInfo healthInfo)
        {
            imageSlider.fillAmount = healthInfo.CurrentHealth / healthInfo.MaxHealth;
        }
    }
}

