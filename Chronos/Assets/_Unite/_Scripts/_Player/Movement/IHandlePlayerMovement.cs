namespace Unite.Player
{
    public interface IHandlePlayerMovement
    {
        public void PerformSetup(PlayerStatsHandler playerStatsHandler);
        public void UpdateSpeedFromStats();
        public void ModifySpeed(float modifier);
        public void ToggleMovement(bool toggle);
    }
}