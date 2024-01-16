using Unite.TimeStop;
using UnityEngine;

namespace Unite.Enemies
{
    public class TimeStopEnemy : MonoBehaviour, ITimeStopSubscriber
    {
        private Enemy enemy;

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

            if (enemy.StrafeHandler == null) return;
            enemy.StrafeHandler.enabled = !isTimeStopped;
            if (enemy.ProjectileShooter == null) return;
            enemy.ProjectileShooter.enabled = !isTimeStopped;
        }
    }
}