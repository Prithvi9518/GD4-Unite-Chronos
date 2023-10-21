using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "PlayerInRangeCondition", menuName = "Unite/Scriptable Objects/AI/Conditions/PlayerInRangeCondition")]
    public class PlayerInRangeCondition : Condition
    {
        private PrototypeEnemyStateMachine enemy;

        public override bool VerifyCondition(IStateMachine stateMachine)
        {
            if (enemy == null)
            {
                enemy = stateMachine as PrototypeEnemyStateMachine;
            }

            return Vector3.Distance(enemy.Target.position,
                enemy.transform.position) <= enemy.DetectionRange;
        }
    }
}