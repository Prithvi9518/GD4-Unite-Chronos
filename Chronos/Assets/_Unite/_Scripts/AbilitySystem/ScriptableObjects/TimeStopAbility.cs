using Unite.EventSystem;
using Unite.TimeStop;
using UnityEngine;

namespace Unite.AbilitySystem
{
    /// <summary>
    /// Handles activation and de-activation of the time stop ability.
    /// Calls the TimeStopManager singleton to handle stopping time.
    ///
    /// <seealso cref="TimeStopManager"/>
    /// </summary>
    [CreateAssetMenu(fileName ="TimeStopAbility", menuName ="Abilities/Time Stop")]
    public class TimeStopAbility : AbilityData
    {
        [SerializeField]
        private GameEvent onUpdateAnalytics;
        
        public override void Activate()
        {
            TimeStopManager.Instance.TriggerTimeStop(true);
            onUpdateAnalytics.Raise();
            onAbilityActivate.Raise();
        }

        public override void Deactivate()
        {
            TimeStopManager.Instance.TriggerTimeStop(false);
            onAbilityDeactivate.Raise();
        }
    }
}

