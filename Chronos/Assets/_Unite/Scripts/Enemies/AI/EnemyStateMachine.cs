using UnityEngine;
using UnityEngine.AI;

namespace Unite
{
    public class EnemyStateMachine : MonoBehaviour, IStateMachine
    {
        [SerializeField]
        private Transform target;

        private State startingState;

        // Dummy state used to remain in the same state if needed
        private State remainState;

        private State currentState;

        private NavMeshAgent navMeshAgent;

        private EnemyAttackHandler enemyAttackHandler;
        private EnemyDetectionHandler enemyDetectionHandler;

        public Transform Target => target;
        public NavMeshAgent Agent => navMeshAgent;
        public EnemyAttackHandler AttackHandler => enemyAttackHandler;
        public EnemyDetectionHandler DetectionHandler => enemyDetectionHandler;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyAttackHandler = GetComponent<EnemyAttackHandler>();
            enemyDetectionHandler = GetComponent<EnemyDetectionHandler>();
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }

        public void PerformSetup(EnemyData enemyData)
        {
            startingState = enemyData.StartState;
            remainState = enemyData.RemainState;

            currentState = startingState;
        }

        public void SetCurrentState(State state)
        {
            if (state == remainState) return;

            currentState = state;
        }
    }
}