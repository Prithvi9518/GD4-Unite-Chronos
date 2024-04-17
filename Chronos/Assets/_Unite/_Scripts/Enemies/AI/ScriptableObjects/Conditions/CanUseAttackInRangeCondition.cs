using System.Collections.Generic;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    /// <summary>
    /// Condition used by the enemy state machine to check whether the target is in range of the provided attack, and
    /// whether the attack is currently not in cooldown.
    /// </summary>
    [CreateAssetMenu(fileName = "CanUseAttackInRangeCondition", menuName = "AI/Conditions/CanUseAttackInRangeCondition")]
    public class CanUseAttackInRangeCondition : Condition
    {
        [SerializeField]
        private AttackData attackData;
        public override bool VerifyCondition(BaseStateMachine baseStateMachine)
        {
            EnemyAttackHandler attackHandler = baseStateMachine.GetComponent<EnemyAttackHandler>();
            EnemyDetectionHandler detectionHandler = baseStateMachine.GetComponent<EnemyDetectionHandler>();
            
            Attack attack = attackHandler.Attacks.GetValueOrDefault(attackData.name, null);

            return attack.CanUseAttack() && attackData.WithinAttackRange(baseStateMachine.transform, detectionHandler.Target);
        }
    }
}