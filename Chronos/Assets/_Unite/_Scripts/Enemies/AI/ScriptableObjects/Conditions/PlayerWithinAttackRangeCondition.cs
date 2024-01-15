using System.Collections.Generic;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "PlayerWithinAttackRangeCondition", menuName = "AI/Conditions/PlayerWithinAttackRange")]
    public class PlayerWithinAttackRangeCondition : Condition
    {
        [SerializeField]
        private AttackName attackName;

        public override bool VerifyCondition(BaseStateMachine baseStateMachine)
        {
            EnemyStateMachine enemy = baseStateMachine as EnemyStateMachine;

            Attack attack = enemy.AttackHandler.Attacks.GetValueOrDefault(attackName, null);

            return attack.AttackData.WithinAttackRange(enemy.transform, enemy.DetectionHandler.Target);
        }
    }
}