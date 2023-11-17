using UnityEngine;

namespace Unite.StatePattern
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

