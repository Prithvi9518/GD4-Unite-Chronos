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
        }

        public void UpdateHealthBar(PlayerHealthInfo healthInfo)
        {
            slider.value = healthInfo.CurrentHealth / healthInfo.MaxHealth;
        }
    }
}

