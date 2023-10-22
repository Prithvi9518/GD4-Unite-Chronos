using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName ="BasicMeleeAttack", menuName ="Unite/Scriptable Objects/AI/Actions/Basic Melee Attack")]
    public class BasicMeleeAttackAction : Action
    {
        [SerializeField]
        private AttackData attackData;

        private EnemyStateMachine enemy;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            if (enemy == null)
                enemy = stateMachine as EnemyStateMachine;

            if (!attackData.CanUseAttack(enemy)) return;

            attackData.DoAttack(enemy);
        }
    }
}

