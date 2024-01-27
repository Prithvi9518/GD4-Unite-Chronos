using Unite.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        //private Slider slider;
        [SerializeField] private Image imageSlider;
        
        private void Awake()
        {
            //slider = GetComponent<Slider>();
            //imageSlider = GetComponent<Image>();
        }

        public void UpdateHealthBar(HealthInfo healthInfo)
        {
            //slider.value = healthInfo.CurrentHealth / healthInfo.MaxHealth;
            imageSlider.fillAmount = healthInfo.CurrentHealth / healthInfo.MaxHealth;
        }
    }
}

