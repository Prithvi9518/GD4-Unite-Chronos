using Unite.UI;
using UnityEngine;

namespace Unite.Enemies
{
    public class EnemyUIHandler : MonoBehaviour
    {
        [SerializeField] 
        private WorldSpaceHealthBar healthBar;

        public void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
    }
}

