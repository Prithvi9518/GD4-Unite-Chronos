using UnityEngine;

namespace Unite
{
    public class TimeStopEnemy : MonoBehaviour, ITimeStopSubscriber
    {
        private Enemy enemy;

        private void OnEnable()
        {
            TimeStopManager.Instance.OnToggleTimeStop += HandleTimeStopEvent;
        }

        private void OnDisable()
        {
            if (TimeStopManager.Instance == null) return;
            TimeStopManager.Instance.OnToggleTimeStop -= HandleTimeStopEvent;
        }

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            enemy.Agent.enabled = !isTimeStopped;
            enemy.AnimationHandler.ToggleAnimator(!isTimeStopped);
            enemy.StateMachine.enabled = !isTimeStopped;

            enemy.Damager.ToggleDelayDeath(isTimeStopped);
        }
    }
}