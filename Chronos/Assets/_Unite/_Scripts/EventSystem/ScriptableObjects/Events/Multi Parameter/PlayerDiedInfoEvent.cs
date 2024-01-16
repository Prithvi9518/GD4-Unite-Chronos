using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "PlayerDiedInfoEvent", menuName = "Events/PlayerDiedInfoEvent")]
    public class PlayerDiedInfoEvent : ParameterisedGameEvent<PlayerDiedInfo>
    {
    }
}