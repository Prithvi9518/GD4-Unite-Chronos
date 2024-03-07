using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "LookAtTargetAction", menuName = "AI/Actions/LookAtTargetAction")]
    public class LookAtTargetAction : FSMAction
    {
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            EnemyDetectionHandler detectionHandler = baseStateMachine.GetComponent<EnemyDetectionHandler>();
            Vector3 targetPos = detectionHandler.Target.position;
            Vector3 targetXZ = new Vector3(targetPos.x, detectionHandler.transform.position.y, targetPos.z);
            baseStateMachine.transform.LookAt(targetXZ);
        }
    }
}