using UnityEngine;

namespace Unite
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth;

        [SerializeField]
        private float currentHealth;

        public float MaxHealth
        {
            get => maxHealth;
            set
            {
                if (value > 0)
                    maxHealth = value;
            }
        }

        private void Awake()
        {
            currentHealth = MaxHealth;
        }

        public float GetHealth()
        {
            return currentHealth;
        }

        public void AddHealth(float amount)
        {
            currentHealth = (currentHealth + amount <= maxHealth) ? currentHealth + amount : maxHealth;
        }

        public void DecreaseHealth(float amount)
        {
            if (currentHealth == 0) return;
            currentHealth = (currentHealth - amount >= 0) ? currentHealth - amount : 0;
        }
    }
}

