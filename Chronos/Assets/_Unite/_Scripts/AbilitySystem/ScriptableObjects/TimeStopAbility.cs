using Unite.TimeStop;
using UnityEngine;

namespace Unite.AbilitySystem
{
    [CreateAssetMenu(fileName ="TimeStopAbility", menuName ="Unite/Scriptable Objects/Abilities/Time Stop")]
    public class TimeStopAbility : AbilityData
    {
        public override void Activate()
        {
            TimeStopManager.Instance.TriggerTimeStop(true);
        }

        public override void Deactivate()
        {
            TimeStopManager.Instance.TriggerTimeStop(false);
        }
    }
}

