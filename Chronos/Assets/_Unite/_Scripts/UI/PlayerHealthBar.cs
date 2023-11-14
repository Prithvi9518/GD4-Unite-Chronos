using Unite.Core.EventSystem;
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

        public void UpdateHealthBar(PlayerHealthInfo healthInfo)
        {
            slider.value = healthInfo.CurrentHealth / healthInfo.MaxHealth;
        }
    }
}

