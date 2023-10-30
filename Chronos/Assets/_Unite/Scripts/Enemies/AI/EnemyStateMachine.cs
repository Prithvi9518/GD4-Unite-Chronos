using UnityEngine;
using UnityEngine.AI;

namespace Unite
{
    public class EnemyStateMachine : MonoBehaviour, IStateMachine
    {
        private State startingState;

        // Dummy state used to remain in the same state if needed
        private State remainState;

        private State currentState;

        private NavMeshAgent navMeshAgent;

        private EnemyAttackHandler enemyAttackHandler;
        private EnemyDetectionHandler enemyDetectionHandler;
        private EnemyAnimationHandler animationHandler;

        public NavMeshAgent Agent => navMeshAgent;
        public EnemyAttackHandler AttackHandler => enemyAttackHandler;
        public EnemyDetectionHandler DetectionHandler => enemyDetectionHandler;
        public EnemyAnimationHandler AnimationHandler => animationHandler;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyAttackHandler = GetComponent<EnemyAttackHandler>();
            enemyDetectionHandler = GetComponent<EnemyDetectionHandler>();
            animationHandler = GetComponent<EnemyAnimationHandler>();
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
            currentState.EnterState(this);
        }

        public void SetCurrentState(State state)
        {
            if (state == remainState) return;

            currentState.ExitState(this);
            currentState = state;
            currentState.EnterState(this);
        }
    }
}