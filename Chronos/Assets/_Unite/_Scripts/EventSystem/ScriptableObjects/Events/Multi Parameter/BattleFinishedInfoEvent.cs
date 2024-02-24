using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "BattleFinishedInfoEvent", menuName = "Events/Battle Finished Info")]
    public class BattleFinishedInfoEvent : ParameterisedGameEvent<BattleFinishedInfo>
    {
    }
}