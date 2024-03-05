using UnityEngine;

namespace Unite.StatePattern
{
    /// <summary>
    /// An FSMAction represents instructions that are performed when a state is active within a state machine.
    /// Make individual classes for actions and inherit from this class. For example: IdleAction, PatrolAction, etc.
    ///
    /// <seealso cref="State"/>
    /// </summary>
    public abstract class FSMAction : ScriptableObject
    {
        public abstract void ExecuteAction(BaseStateMachine baseStateMachine);
    }
}