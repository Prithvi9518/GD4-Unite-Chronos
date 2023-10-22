using UnityEngine;

namespace Unite
{
    public abstract class AttackData : ScriptableObject, ITimeStopSubscriber
    {
        [SerializeField]
        protected float attackCooldown;

        [SerializeField]
        private float damage;

        protected float timeWhenLastAttacked;
        protected bool isPaused;

        public abstract bool CanUseAttack(EnemyStateMachine enemy);
        public abstract void DoAttack(EnemyStateMachine enemy);

        private void OnEnable()
        {
            timeWhenLastAttacked = 0;
            TimeStopManager.Instance.ToggleTimeStop += HandleTimeStopEvent;
        }

        private void OnDisable()
        {
            timeWhenLastAttacked = 0;
            TimeStopManager.Instance.ToggleTimeStop -= HandleTimeStopEvent;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            isPaused = isTimeStopped;
        }
    }
}

