using Unite.TimeStop;
using UnityEngine;

namespace Unite.Enemies
{
    /// <summary>
    /// Handles the pause/un-pause of enemy logic when time is stopped.
    /// </summary>
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