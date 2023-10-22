using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName ="TimeStopAbility", menuName ="Unite/Scriptable Objects/Abilities/Time Stop")]
    public class TimeStopAbility : AbilityData
    {
        private TimeStopManager timeStopManager;

        public override void Activate()
        {
            if(timeStopManager == null)
            {
                timeStopManager = FindObjectOfType<TimeStopManager>();
            }

            timeStopManager.TriggerTimeStop(true);
        }

        public override void Deactivate()
        {
            if (timeStopManager == null)
            {
                timeStopManager = FindObjectOfType<TimeStopManager>();
            }

            timeStopManager.TriggerTimeStop(false);
        }
    }
}

