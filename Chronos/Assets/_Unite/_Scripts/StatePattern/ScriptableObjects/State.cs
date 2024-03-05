using UnityEngine;

namespace Unite.StatePattern
{
    /// <summary>
    /// Represents one individual state within a state machine.
    /// We can create scriptable object assets to represent various states, and assign actions to perform during that state.
    /// We can also assign any transitions to other states by checking for conditions.
    ///
    /// <seealso cref="FSMAction"/>
    /// <seealso cref="Condition"/>
    /// <seealso cref="StateTransition"/>
    /// <seealso cref="BaseStateMachine"/>
    /// </summary>

    [CreateAssetMenu(fileName = "State", menuName = "AI/State")]
    public class State : ScriptableObject
    {
        [Header("Actions")]
        [Tooltip("Actions performed only once when entering the state.")]
        [SerializeField]
        private FSMAction[] enterActions;

        [Tooltip("Actions performed on every update.")]
        [SerializeField]
        private FSMAction[] updateActions;

        [Tooltip("Actions performed only once when exiting the state.")]
        [SerializeField]
        private FSMAction[] exitActions;

        [Header("Condition-Based Transitions")]
        [SerializeField]
        private StateTransition[] conditionBasedTransitions;

        [Header("Event-Based Transitions")]
        [SerializeField]
        private StateEventTransition[] eventTransitions;

        public void EnterState(BaseStateMachine baseStateMachine)
        {
            PerformEnterActions(baseStateMachine);
        }

        public void UpdateState(BaseStateMachine baseStateMachine)
        {
            PerformUpdateActions(baseStateMachine);
            CheckConditionalTransitions(baseStateMachine);
        }

        public void ExitState(BaseStateMachine baseStateMachine)
        {
            PerformExitActions(baseStateMachine);
        }

        public void CheckEventTransitions(BaseStateMachine baseStateMachine, StateEvent stateEvent)
        {
            foreach (StateEventTransition transition in eventTransitions)
            {
                if (transition.Event == stateEvent)
                {
                    baseStateMachine.SetCurrentState(transition.ToState);
                }
            }
        }

        private void PerformEnterActions(BaseStateMachine baseStateMachine)
        {
            foreach (FSMAction action in enterActions)
            {
                action.ExecuteAction(baseStateMachine);
            }
        }

        private void PerformUpdateActions(BaseStateMachine baseStateMachine)
        {
            foreach (FSMAction action in updateActions)
            {
                action.ExecuteAction(baseStateMachine);
            }
        }

        private void PerformExitActions(BaseStateMachine baseStateMachine)
        {
            foreach (FSMAction action in exitActions)
            {
                action.ExecuteAction(baseStateMachine);
            }
        }

        private void CheckConditionalTransitions(BaseStateMachine baseStateMachine)
        {
            foreach (StateTransition transition in conditionBasedTransitions)
            {
                bool conditionSatisfied = transition.Condition.VerifyCondition(baseStateMachine);

                if (conditionSatisfied)
                {
                    baseStateMachine.SetCurrentState(transition.TrueState);
                }
                else
                {
                    baseStateMachine.SetCurrentState(transition.FalseState);
                }
            }
        }
    }
}