using UnityEngine;

namespace Unite.StatePattern
{
    /// <summary>
    /// This class represents the condition required to transition from one state to another.
    /// Create individual conditions that inherit from this abstract class. For example: PlayerDetectedCondition, Phase2Condition, etc.
    ///
    /// <seealso cref="State"/>
    /// </summary>
    public abstract class Condition : ScriptableObject
    {
        public abstract bool VerifyCondition(IStateMachine stateMachine);
    }
}