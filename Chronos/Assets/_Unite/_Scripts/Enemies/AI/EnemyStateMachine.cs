using Unite.StatePattern;
using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.AI
{
    public class EnemyStateMachine : MonoBehaviour, IStateMachine
    {
        private State startingState;

        // Dummy state used to remain in the same state if needed
        private State remainState;

        private State currentState;

        private Enemy enemy;

        public NavMeshAgent Agent => enemy.Agent;
        public Collider Collider => enemy.Collider;
        public EnemyDetectionHandler DetectionHandler => enemy.DetectionHandler;
        public EnemyAttackHandler AttackHandler => enemy.AttackHandler;
        public EnemyAnimationHandler AnimationHandler => enemy.AnimationHandler;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }

        public void PerformSetup(State start, State remain)
        {
            startingState = start;
            remainState = remain;

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

        public void TriggerStateEvent(StateEvent stateEvent)
        {
            currentState.CheckEventTransitions(this, stateEvent);
        }
    }
}