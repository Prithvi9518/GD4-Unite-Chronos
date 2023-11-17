using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "PlayerHealthInfoGameEvent", menuName = "Unite/Scriptable Objects/Events/HealthInfo")]
    public class HealthInfoEvent : ParameterisedGameEvent<HealthInfo>
    {
    }
}