using System.Collections.Generic;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "BasicAttack", menuName = "AI/Actions/Basic Attack")]
    public class BasicAttackAction : Action
    {
        [SerializeField]
        private AttackName attackName;

        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            Attack attack = baseStateMachine.GetComponent<EnemyAttackHandler>().
                Attacks.GetValueOrDefault(attackName, null);

            if (!attack.CanUseAttack()) return;

            attack.DoAttack(baseStateMachine.GetComponent<Enemy>());
        }
    }
}