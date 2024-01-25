using UnityEngine;

namespace Unite.StatePattern
{
    /// <summary>
    /// An Action represents instructions that are performed when a state is active within a state machine.
    /// Make individual classes for actions and inherit from this class. For example: IdleAction, PatrolAction, etc.
    ///
    /// <seealso cref="State"/>
    /// </summary>
    public abstract class Action : ScriptableObject
    {
        public abstract void ExecuteAction(BaseStateMachine baseStateMachine);
    }
}