namespace Unite
{
    /// <summary>
    /// All state machines created must implement this interface.
    /// This allows us to pass in any variation of state machine into an executable Action object, or a state transtition object.
    ///
    /// <seealso cref="State"/>
    /// </summary>
    public interface IStateMachine
    {
        public void SetCurrentState(State state);
    }
}