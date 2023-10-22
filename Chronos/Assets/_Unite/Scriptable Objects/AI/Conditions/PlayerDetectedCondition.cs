using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "PlayerDetectedCondition", menuName = "Unite/Scriptable Objects/AI/Conditions/PlayerDetectedCondition")]
    public class PlayerDetectedCondition : Condition
    {
        private EnemyStateMachine enemy;

        public override bool VerifyCondition(IStateMachine stateMachine)
        {
            if (enemy == null)
            {
                enemy = stateMachine as EnemyStateMachine;
            }

            return enemy.TargetDetector.IsTargetDetected(enemy.Target);
        }
    }
}