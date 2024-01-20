using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "LookAtTargetAction", menuName = "AI/Actions/LookAtTargetAction")]
    public class LookAtTargetAction : Action
    {
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            EnemyDetectionHandler detectionHandler = baseStateMachine.GetComponent<EnemyDetectionHandler>();
            baseStateMachine.transform.LookAt(detectionHandler.Target);
        }
    }
}