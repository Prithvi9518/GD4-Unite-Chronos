namespace Unite.Player
{
    /// <summary>
    /// Must be implemented by any script that implements player movement.
    /// Used to maintain backward-compatibility for the old player controller,
    /// hence allowing for the switch between old and new controllers.
    ///
    /// <seealso cref="PlayerController"/>
    /// <seealso cref="PlayerMovementHandler"/>
    /// </summary>
    public interface IHandlePlayerMovement
    {
        public void PerformSetup(PlayerStatsHandler playerStatsHandler);
        public void UpdateSpeedBoostFromStats();
        public void ModifySpeed(float modifier);
        public void EnableMovement();
        public void DisableMovement();
    }
}