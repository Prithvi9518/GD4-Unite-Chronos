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

            float distance = Vector3.Distance(enemy.transform.position, enemy.DetectionHandler.Target.position);

            return distance <= attack.AttackData.MaxAttackRange &&
                   distance >= attack.AttackData.MinAttackRange;
        }
    }
}