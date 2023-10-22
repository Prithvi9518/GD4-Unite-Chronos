using UnityEngine.AI;

namespace Unite
{
    public class TimeStopEnemy : TimeStopSubscriber
    {
        private EnemyStateMachine enemyStateMachine;
        private NavMeshAgent agent;
        private EnemyDamager enemyDamager;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            enemyStateMachine = GetComponent<EnemyStateMachine>();
            enemyDamager = GetComponent<EnemyDamager>();
        }

        protected override void HandleTimeStopEvent(bool isTimeStopped)
        {
            agent.enabled = !isTimeStopped;
            enemyStateMachine.enabled = !isTimeStopped;

            enemyDamager.ToggleStoredDamage(isTimeStopped);

            if (!isTimeStopped)
                enemyDamager.ApplyStoredDamage();
        }

    }
}

