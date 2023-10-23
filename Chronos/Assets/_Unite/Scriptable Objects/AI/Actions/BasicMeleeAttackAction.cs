using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "BasicMeleeAttack", menuName = "Unite/Scriptable Objects/AI/Actions/Basic Melee Attack")]
    public class BasicMeleeAttackAction : Action
    {
        [SerializeField]
        private AttackType attackType;

        private EnemyStateMachine enemy;
        private AttackData attack;

        public override void ExecuteAction(IStateMachine stateMachine)
        {
            if (enemy == null)
                enemy = stateMachine as EnemyStateMachine;

            if (attack == null)
            {
                enemy.AttackHandler.Attacks.TryGetValue(attackType, out attack);
            }

            if (!attack.CanUseAttack(enemy)) return;

            attack.DoAttack(enemy);
        }

        private void OnEnable()
        {
            enemy = null;
            attack = null;
        }

        private void OnDisable()
        {
            enemy = null;
            attack = null;
        }
    }
}