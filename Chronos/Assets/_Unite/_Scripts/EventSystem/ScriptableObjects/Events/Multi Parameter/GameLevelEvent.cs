using Unite.Core.Game;
using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "GameLevelEvent", menuName = "Events/Game Level")]
    public class GameLevelEvent : ParameterisedGameEvent<GameLevel>
    {
    }
}