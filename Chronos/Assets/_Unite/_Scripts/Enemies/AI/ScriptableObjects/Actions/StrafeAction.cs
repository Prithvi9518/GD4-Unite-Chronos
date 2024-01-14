using Unite.Enemies.Movement;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "StrafeAction", menuName = "AI/Actions/Strafe")]
    public class StrafeAction : Action
    {
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            StrafeHandler strafeHandler = baseStateMachine.GetComponent<StrafeHandler>();
            
            strafeHandler.ToggleStrafing(true);
        }
    }
}