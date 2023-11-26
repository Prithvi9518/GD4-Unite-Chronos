using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "PlayerEvent", menuName = "Events/Player")]
    public class PlayerEvent : ParameterisedGameEvent<Player.Player>
    {
    }
}