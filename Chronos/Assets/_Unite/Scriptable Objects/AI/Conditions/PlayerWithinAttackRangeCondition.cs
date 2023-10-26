using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "PlayerWithinAttackRangeCondition", menuName = "Unite/Scriptable Objects/AI/Conditions/PlayerWithinAttackRange")]
    public class PlayerWithinAttackRangeCondition : Condition
    {
        [SerializeField]
        private AttackType attackType;

        public override bool VerifyCondition(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;

            Attack attack = enemy.AttackHandler.Attacks.GetValueOrDefault(attackType, null);

            return Vector3.Distance(enemy.transform.position,
                enemy.Target.transform.position) <= attack.AttackData.AttackRange;
        }
    }
}