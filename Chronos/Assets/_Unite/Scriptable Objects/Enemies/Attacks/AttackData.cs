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

        public AttackType AttackType => attackType;
        public float AttackRange => attackRange;

        public float AttackCooldown => attackCooldown;

        public abstract void Attack(EnemyStateMachine enemy);
    }
}