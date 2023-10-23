using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "PlayerWithinAttackRangeCondition", menuName = "Unite/Scriptable Objects/AI/Conditions/PlayerWithinAttackRange")]
    public class PlayerWithinAttackRangeCondition : Condition
    {
        [SerializeField]
        private AttackType attackType;

        private EnemyStateMachine enemy;
        private AttackData attack;


        public override bool VerifyCondition(IStateMachine stateMachine)
        {
            if (enemy == null)
                enemy = stateMachine as EnemyStateMachine;

            if(attack == null)
            {
                enemy.AttackHandler.Attacks.TryGetValue(attackType, out attack);
            }

            return Vector3.Distance(enemy.transform.position,
                enemy.Target.transform.position) <= attack.AttackRange;
        }
    }
}