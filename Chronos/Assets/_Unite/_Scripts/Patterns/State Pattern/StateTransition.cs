using System;
using UnityEngine;

namespace Unite
{
    /// <summary>
    /// A StateTransition represents the conditions needed to transition from one state to another.
    /// On updates, the active state will check if a transition condition is met.
    /// We can specify the states that will be triggered when the condition is true or false.
    ///
    /// <seealso cref="State"/>
    /// </summary>
    [Serializable]
    public class StateTransition
    {
        [SerializeField] private Condition condition;
        [SerializeField] private State trueState;
        [SerializeField] private State falseState;

        public Condition Condition => condition;
        public State TrueState => trueState;
        public State FalseState => falseState;
    }
}