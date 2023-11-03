using UnityEngine;

namespace Unite
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth;

        [SerializeField]
        private float currentHealth;

        // If we are setting up an entity using a Scriptable Object file
        // then we won't set up initial values in the Start method to avoid race conditions.
        [SerializeField]
        private bool setupFromScriptableObject;

        public float MaxHealth
        {
            get => maxHealth;
            set
            {
                if (value > 0)
                    maxHealth = value;
            }
        }

        public void Start()
        {
            if (setupFromScriptableObject) return;

            currentHealth = maxHealth;
        }

        public float CurrentHealth => currentHealth;

        public void ResetHealth()
        {
            currentHealth = maxHealth;
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