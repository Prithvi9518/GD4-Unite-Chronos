using UnityEngine;
using UnityEngine.AI;

namespace Unite
{
    public class TimeStopEnemy : MonoBehaviour, ITimeStopSubscriber
    {
        private EnemyStateMachine enemyStateMachine;
        private NavMeshAgent agent;
        private EnemyDamager enemyDamager;

        private void OnEnable()
        {
            TimeStopManager.Instance.ToggleTimeStop += HandleTimeStopEvent;
        }

        private void OnDisable()
        {
            if (TimeStopManager.Instance == null) return;
            TimeStopManager.Instance.ToggleTimeStop -= HandleTimeStopEvent;
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyStateMachine = GetComponent<EnemyStateMachine>();
            enemyDamager = GetComponent<EnemyDamager>();
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            agent.enabled = !isTimeStopped;
            enemyStateMachine.AnimationHandler.ToggleAnimator(!isTimeStopped);
            enemyStateMachine.enabled = !isTimeStopped;

            enemyDamager.ToggleDelayDeath(isTimeStopped);
        }
    }
}