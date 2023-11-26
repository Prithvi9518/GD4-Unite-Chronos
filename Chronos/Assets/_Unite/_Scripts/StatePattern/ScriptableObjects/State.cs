using UnityEngine;

namespace Unite.StatePattern
{
    /// <summary>
    /// Represents one individual state within a state machine.
    /// We can create scriptable object assets to represent various states, and assign actions to perform during that state.
    /// We can also assign any transitions to other states by checking for conditions.
    ///
    /// <seealso cref="Action"/>
    /// <seealso cref="Condition"/>
    /// <seealso cref="StateTransition"/>
    /// <seealso cref="IStateMachine"/>
    /// </summary>

    [CreateAssetMenu(fileName = "State", menuName = "AI/State")]
    public class State : ScriptableObject
    {
        [Header("Actions")]
        [Tooltip("Actions performed only once when entering the state.")]
        [SerializeField]
        private Action[] enterActions;

        [Tooltip("Actions performed on every update.")]
        [SerializeField]
        private Action[] updateActions;

        [Tooltip("Actions performed only once when exiting the state.")]
        [SerializeField]
        private Action[] exitActions;

        [Header("Condition-Based Transitions")]
        [SerializeField]
        private StateTransition[] conditionBasedTransitions;

        [Header("Event-Based Transitions")]
        [SerializeField]
        private StateEventTransition[] eventTransitions;

        public void EnterState(IStateMachine stateMachine)
        {
            PerformEnterActions(stateMachine);
        }

        public void UpdateState(IStateMachine stateMachine)
        {
            PerformUpdateActions(stateMachine);
            CheckConditionalTransitions(stateMachine);
        }

        public void ExitState(IStateMachine stateMachine)
        {
            PerformExitActions(stateMachine);
        }

        public void CheckEventTransitions(IStateMachine stateMachine, StateEvent stateEvent)
        {
            foreach (StateEventTransition transition in eventTransitions)
            {
                if (transition.Event == stateEvent)
                {
                    stateMachine.SetCurrentState(transition.ToState);
                }
            }
        }

        private void PerformEnterActions(IStateMachine stateMachine)
        {
            foreach (Action action in enterActions)
            {
                action.ExecuteAction(stateMachine);
            }
        }

        private void PerformUpdateActions(IStateMachine stateMachine)
        {
            foreach (Action action in updateActions)
            {
                action.ExecuteAction(stateMachine);
            }
        }

        private void PerformExitActions(IStateMachine stateMachine)
        {
            foreach (Action action in exitActions)
            {
                action.ExecuteAction(stateMachine);
            }
        }

        private void CheckConditionalTransitions(IStateMachine stateMachine)
        {
            foreach (StateTransition transition in conditionBasedTransitions)
            {
                bool conditionSatisfied = transition.Condition.VerifyCondition(stateMachine);

                if (conditionSatisfied)
                {
                    stateMachine.SetCurrentState(transition.TrueState);
                }
                else
                {
                    stateMachine.SetCurrentState(transition.FalseState);
                }
            }
        }
    }
}