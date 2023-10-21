using UnityEngine;
using UnityEngine.AI;

namespace Unite
{
    public class PrototypeEnemyStateMachine : MonoBehaviour, IStateMachine
    {
        [SerializeField]
        private State startingState;

        private State currentState;

        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            currentState = startingState;
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }

        public void SetCurrentState(State state)
        {
            currentState = state;
        }
    }
}