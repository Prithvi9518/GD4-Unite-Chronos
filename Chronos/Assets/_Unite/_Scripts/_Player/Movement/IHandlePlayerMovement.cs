namespace Unite.Player
{
    public interface IHandlePlayerMovement
    {
        public void UpdateSpeedFromStats();
        public void ModifySpeed(float modifier);
        public void ToggleMovement(bool toggle);
    }
}