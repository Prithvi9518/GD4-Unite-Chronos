using System.Collections;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "SwitchToStateAction", menuName = "AI/Actions/SwitchToStateAction")]
    public class SwitchToStateAction : Action
    {
        [SerializeField]
        private State state;

        [SerializeField]
        private bool hasDelay;
        
        [SerializeField]
        private float delayInSeconds;
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            if (!hasDelay)
            {
                baseStateMachine.SetCurrentState(state);
                return;
            }
            
            baseStateMachine.StartCoroutine(SwitchToState(baseStateMachine));
        }

        private IEnumerator SwitchToState(BaseStateMachine stateMachine)
        {
            yield return new WaitForSeconds(delayInSeconds);
            stateMachine.SetCurrentState(state);
        }
    }
}