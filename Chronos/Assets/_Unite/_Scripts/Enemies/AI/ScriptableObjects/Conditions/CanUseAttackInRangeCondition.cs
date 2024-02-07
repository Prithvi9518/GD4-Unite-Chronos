using System.Collections.Generic;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
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