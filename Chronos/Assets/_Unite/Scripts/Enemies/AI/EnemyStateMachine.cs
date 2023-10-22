using UnityEngine;
using UnityEngine.AI;

namespace Unite
{
    [RequireComponent(typeof(NavMeshAgent), typeof(IDetectTarget))]
    public class EnemyStateMachine : MonoBehaviour, IStateMachine
    {
        [SerializeField]
        private EnemyData enemyData;

        [SerializeField]
        private Transform target;

        [SerializeField]
        private State startingState;

        // Dummy state used to remain in the same state if needed
        [SerializeField]
        private State remainState;

        private State currentState;
        private NavMeshAgent navMeshAgent;
        private IDetectTarget targetDetector;

        public EnemyData EnemyData => enemyData;
        public Transform Target => target;
        public NavMeshAgent Agent => navMeshAgent;
        public IDetectTarget TargetDetector => targetDetector;

        private void Awake()
        {
            currentState = startingState;
            navMeshAgent = GetComponent<NavMeshAgent>();
            targetDetector = GetComponent<IDetectTarget>();
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
    }
}