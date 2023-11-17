using Unite.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        private Slider slider;
        
        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        public void UpdateHealthBar(HealthInfo healthInfo)
        {
            slider.value = healthInfo.CurrentHealth / healthInfo.MaxHealth;
        }
    }
}

