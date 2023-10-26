using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "BasicMeleeAttack", menuName = "Unite/Scriptable Objects/AI/Actions/Basic Melee Attack")]
    public class BasicMeleeAttackAction : Action
    {
        [SerializeField]
        private AttackType attackType;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;

            Attack attack = enemy.AttackHandler.Attacks.GetValueOrDefault(attackType, null);

            if (!attack.CanUseAttack()) return;

            attack.DoAttack(enemy);
        }
    }
}