using System.Collections.Generic;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "PlayerWithinAttackRangeCondition", menuName = "AI/Conditions/PlayerWithinAttackRange")]
    public class PlayerWithinAttackRangeCondition : Condition
    {
        [SerializeField]
        private AttackData attackData;

        public override bool VerifyCondition(BaseStateMachine baseStateMachine)
        {
            Enemy enemy = baseStateMachine.GetComponent<Enemy>();

            Attack attack = enemy.AttackHandler.Attacks.GetValueOrDefault(attackData.name, null);

            return attack.AttackData.WithinAttackRange(enemy.transform, enemy.DetectionHandler.Target)
                && enemy.DetectionHandler.IsTargetDetected();
        }
    }
}