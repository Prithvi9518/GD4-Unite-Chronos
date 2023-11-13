using UnityEngine;

namespace Unite
{
    [System.Serializable]
    public class StateEventTransition
    {
        [SerializeField]
        private StateEvent stateEvent;
        [SerializeField]
        private State toState;

        public StateEvent Event => stateEvent;
        public State ToState => toState;
    }
}

