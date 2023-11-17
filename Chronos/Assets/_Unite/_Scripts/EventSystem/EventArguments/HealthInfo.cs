namespace Unite.EventSystem
{
    /// <summary>
    /// This struct is passed as an event parameter when raising events related to health
    /// </summary>
    public struct HealthInfo
    {
        public readonly float CurrentHealth;
        public readonly float MaxHealth;

        public HealthInfo(float currentHealth, float maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }
    }
}