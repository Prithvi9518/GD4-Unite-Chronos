namespace Unite.TimeStop
{
    public interface ITimeStopSubscriber
    {
        public void HandleTimeStopEvent(bool isTimeStopped);
    }
}