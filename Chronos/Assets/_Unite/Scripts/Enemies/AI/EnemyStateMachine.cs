using UnityEngine;
using UnityEngine.AI;

namespace Unite
{
    [RequireComponent(typeof(NavMeshAgent), typeof(IDetectTarget))]
    public class EnemyStateMachine : MonoBehaviour, IStateMachine
    {
        private EnemyData enemyData;

        [SerializeField]
        private Transform target;

        private State startingState;

        // Dummy state used to remain in the same state if needed
        private State remainState;

        private State currentState;
        private NavMeshAgent navMeshAgent;
        private CharacterController characterController;
        private IDetectTarget targetDetector;
        private EnemyAttackHandler enemyAttackHandler;

        public EnemyData EnemyData => enemyData;
        public Transform Target => target;
        public NavMeshAgent Agent => navMeshAgent;
        public CharacterController CharacterController => characterController;
        public IDetectTarget TargetDetector => targetDetector;

        public EnemyAttackHandler AttackHandler => enemyAttackHandler;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            characterController = GetComponent<CharacterController>();
            targetDetector = GetComponent<IDetectTarget>();
            enemyAttackHandler = GetComponent<EnemyAttackHandler>();
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }

        public void SetupStates(EnemyData enemyData)
        {
            this.enemyData = enemyData;

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