namespace Unite
{
    public interface ITimeStopSubscriber
    {
        public void HandleTimeStopEvent(bool isTimeStopped);
    }
}