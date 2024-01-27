using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "PlayerWithinSpecifiedRangeCondition", menuName = "AI/Conditions/PlayerWithinSpecifiedRangeCondition")]
    public class PlayerWithinSpecifiedRangeCondition : Condition
    {
        [SerializeField]
        private float minRange;

        [SerializeField]
        private float maxRange;

        public override bool VerifyCondition(BaseStateMachine baseStateMachine)
        {
            EnemyDetectionHandler detectionHandler = baseStateMachine.GetComponent<EnemyDetectionHandler>();

            float distance = Vector3.Distance(baseStateMachine.transform.position, detectionHandler.Target.position);

            return distance >= minRange &&
                   distance <= maxRange;
        }
    }
}