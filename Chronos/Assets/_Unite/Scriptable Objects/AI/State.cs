using UnityEngine;

namespace Unite
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

    [CreateAssetMenu(fileName = "State", menuName = "Unite/Scriptable Objects/AI/State")]
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

        [Header("Transitions")]
        [SerializeField]
        private StateTransition[] transitions;

        public void EnterState(IStateMachine stateMachine)
        {
            PerformEnterActions(stateMachine);
        }

        public void UpdateState(IStateMachine stateMachine)
        {
            PerformUpdateActions(stateMachine);
            CheckTransitions(stateMachine);
        }

        public void ExitState(IStateMachine stateMachine)
        {
            PerformExitActions(stateMachine);
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

        private void CheckTransitions(IStateMachine stateMachine)
        {
            foreach (StateTransition transition in transitions)
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