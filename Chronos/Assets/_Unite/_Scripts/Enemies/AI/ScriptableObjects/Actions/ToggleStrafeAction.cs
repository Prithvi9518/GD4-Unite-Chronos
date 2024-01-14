using Unite.Enemies.Movement;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "ToggleStrafeAction", menuName = "AI/Actions/Toggle Strafe")]
    public class ToggleStrafeAction : Action
    {
        [SerializeField]
        private bool active;
        
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            StrafeHandler strafeHandler = baseStateMachine.GetComponent<StrafeHandler>();
            
            strafeHandler.ToggleStrafing(active);
        }
    }
}