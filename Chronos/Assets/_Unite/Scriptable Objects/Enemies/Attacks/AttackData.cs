using UnityEngine;

namespace Unite
{
    public abstract class AttackData : ScriptableObject
    {
        [SerializeField]
        private AttackType attackType;

        [SerializeField]
        private float damage;

        [SerializeField]
        private float attackRange;

        [SerializeField]
        protected float attackCooldown;

        protected float timeWhenLastAttacked;

        public AttackType AttackType => attackType;
        public float AttackRange => attackRange;

        public abstract bool CanUseAttack(EnemyStateMachine enemy);

        public abstract void DoAttack(EnemyStateMachine enemy);

        private void OnEnable()
        {
            timeWhenLastAttacked = 0;
        }

        private void OnDisable()
        {
            timeWhenLastAttacked = 0;
        }
    }
}