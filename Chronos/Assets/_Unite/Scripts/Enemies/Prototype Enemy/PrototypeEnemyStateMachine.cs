using UnityEngine;
using UnityEngine.AI;

namespace Unite
{
    public class PrototypeEnemyStateMachine : MonoBehaviour, IStateMachine
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private float detectionRange;

        [SerializeField]
        private State startingState;

        // Dummy state used to remain in the same state if needed
        [SerializeField]
        private State remainState;

        private State currentState;
        private NavMeshAgent navMeshAgent;

        public Transform Target => target;
        public float DetectionRange => detectionRange;
        public NavMeshAgent Agent => navMeshAgent;

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
            if (state == remainState) return;

            currentState = state;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}