using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "PlayerHealthInfoGameEvent", menuName = "Events/HealthInfo")]
    public class HealthInfoEvent : ParameterisedGameEvent<HealthInfo>
    {
    }
}