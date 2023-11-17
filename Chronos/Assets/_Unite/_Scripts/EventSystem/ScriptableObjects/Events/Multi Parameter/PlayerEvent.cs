using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "PlayerEvent", menuName = "Unite/Scriptable Objects/Events/Player")]
    public class PlayerEvent : ParameterisedGameEvent<Player.Player>
    {
    }
}