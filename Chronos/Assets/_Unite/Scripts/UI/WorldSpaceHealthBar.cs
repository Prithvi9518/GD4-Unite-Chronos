using UnityEngine;
using UnityEngine.UI;

namespace Unite
{
    public class WorldSpaceHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Transform owner;

        [SerializeField] 
        private Vector3 offset;
        
        private Camera cam;
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            cam = Camera.main;
        }

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            slider.value = currentHealth / maxHealth;
        }

        private void Update()
        {
            transform.parent.rotation = cam.transform.rotation;
            transform.position = owner.position + offset;
        }
    }
}

