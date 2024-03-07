namespace Unite.Player
{
    public interface IHandlePlayerMovement
    {
        public void PerformSetup(PlayerStatsHandler playerStatsHandler);
        public void UpdateSpeedBoostFromStats();
        public void ModifySpeed(float modifier);
        public void EnableMovement();
        public void DisableMovement();
    }
}