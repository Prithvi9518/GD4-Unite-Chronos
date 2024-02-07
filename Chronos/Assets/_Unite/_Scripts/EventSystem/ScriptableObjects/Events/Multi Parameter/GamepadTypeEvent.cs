using Unite.Core.Input;
using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "GamepadTypeEvent", menuName = "Events/Gamepad Type")]
    public class GamepadTypeEvent : ParameterisedGameEvent<GamepadType>
    {
    }
}