using Unite.EventSystem;
using Unite.TimeStop;
using UnityEngine;

namespace Unite.AbilitySystem
{
    [CreateAssetMenu(fileName ="TimeStopAbility", menuName ="Abilities/Time Stop")]
    public class TimeStopAbility : AbilityData
    {
        [SerializeField]
        private GameEvent onUpdateAnalytics;
        
        public override void Activate()
        {
            TimeStopManager.Instance.TriggerTimeStop(true);
            onUpdateAnalytics.Raise();
        }

        public override void Deactivate()
        {
            TimeStopManager.Instance.TriggerTimeStop(false);
        }
    }
}

