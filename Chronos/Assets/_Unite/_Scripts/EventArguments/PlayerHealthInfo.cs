namespace Unite
{
    /// <summary>
    /// This struct is passed as an event parameter when raising events related to player health
    /// </summary>
    public struct PlayerHealthInfo
    {
        public readonly float CurrentHealth;
        public readonly float MaxHealth;

        public PlayerHealthInfo(float currentHealth, float maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }
    }
}