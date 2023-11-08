using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "PlayerDetectedCondition", menuName = "Unite/Scriptable Objects/AI/Conditions/PlayerDetectedCondition")]
    public class PlayerDetectedCondition : Condition
    {
        public override bool VerifyCondition(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;

            return enemy.DetectionHandler.IsTargetDetected();
        }
    }
}