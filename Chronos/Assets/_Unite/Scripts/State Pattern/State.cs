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
        [SerializeField]
        private Action[] actions;

        [SerializeField]
        private StateTransition[] transitions;

        private void UpdateState(IStateMachine stateMachine)
        {
            PerformActions(stateMachine);
            CheckTransitions(stateMachine);
        }

        private void PerformActions(IStateMachine stateMachine)
        {
            foreach (Action action in actions)
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