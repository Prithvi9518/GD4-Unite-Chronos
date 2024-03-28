using Unite.Managers;
using UnityEngine;

namespace Unite.ActionSystem
{
    [CreateAssetMenu(fileName = "SwitchToNextLevelAction", menuName = "Action System/Switch To Next Level Action")]
    public class SwitchToNextLevelAction : ActionSO
    {
        public override void ExecuteAction()
        {
            GameManager.Instance.SwitchToNextLevel();
        }
    }
}