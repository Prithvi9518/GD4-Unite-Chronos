using Unite.Core.Game;
using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "GameStateEvent", menuName = "Events/Game State", order = 0)]
    public class GameStateEvent : ParameterisedGameEvent<GameState>
    {
    }
}