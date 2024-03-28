using Unite.EventSystem;
using UnityEngine;

namespace Unite.ActionSystem
{
    [CreateAssetMenu(fileName = "RaiseGameEventAction", menuName = "Action System/Raise Game Event Action")]
    public class RaiseGameEventAction : ActionSO
    {
        [SerializeField]
        private GameEvent gameEvent;
        
        public override void ExecuteAction()
        {
            gameEvent.Raise();
        }
    }
}