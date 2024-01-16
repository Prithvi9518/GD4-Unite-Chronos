using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "PlayerDetectedCondition", menuName = "AI/Conditions/PlayerDetectedCondition")]
    public class PlayerDetectedCondition : Condition
    {
        public override bool VerifyCondition(BaseStateMachine baseStateMachine)
        {
            EnemyStateMachine enemy = baseStateMachine as EnemyStateMachine;

            return enemy.DetectionHandler.IsTargetDetected();
        }
    }
}