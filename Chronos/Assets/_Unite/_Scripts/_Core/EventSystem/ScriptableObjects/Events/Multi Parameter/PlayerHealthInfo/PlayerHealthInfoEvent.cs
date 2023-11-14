using UnityEngine;

namespace Unite.Core.EventSystem
{
    [CreateAssetMenu(fileName = "PlayerHealthInfoGameEvent", menuName = "Unite/Scriptable Objects/Events/PlayerHealthInfo")]
    public class PlayerHealthInfoEvent : ParameterisedGameEvent<PlayerHealthInfo>
    {
    }
}