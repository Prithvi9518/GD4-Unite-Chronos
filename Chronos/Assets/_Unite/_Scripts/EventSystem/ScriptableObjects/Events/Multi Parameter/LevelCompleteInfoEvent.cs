using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "LevelCompleteInfoEvent", menuName = "Events/Level Complete Info")]
    public class LevelCompleteInfoEvent : ParameterisedGameEvent<LevelCompleteInfo>
    {
    }
}