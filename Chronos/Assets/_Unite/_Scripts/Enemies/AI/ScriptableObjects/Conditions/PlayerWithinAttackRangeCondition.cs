using System.Collections.Generic;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "PlayerWithinAttackRangeCondition", menuName = "Unite/Scriptable Objects/AI/Conditions/PlayerWithinAttackRange")]
    public class PlayerWithinAttackRangeCondition : Condition
    {
        [SerializeField]
        private AttackName attackName;

        public override bool VerifyCondition(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;

            Attack attack = enemy.AttackHandler.Attacks.GetValueOrDefault(attackName, null);

            float distance = Vector3.Distance(enemy.transform.position, enemy.DetectionHandler.Target.position);

            return distance <= attack.AttackData.AttackRange;
        }
    }
}