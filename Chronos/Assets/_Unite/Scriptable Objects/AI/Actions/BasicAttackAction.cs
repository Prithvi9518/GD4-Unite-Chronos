using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "BasicAttack", menuName = "Unite/Scriptable Objects/AI/Actions/Basic Attack")]
    public class BasicAttackAction : Action
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