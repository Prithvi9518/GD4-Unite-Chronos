using UnityEngine;

namespace Unite.Enemies
{
    public abstract class AttackData : ScriptableObject
    {
        [SerializeField]
        private AttackName attackName;

        [SerializeField]
        private float damage;

        [SerializeField]
        protected float attackRange;
        
        [SerializeField]
        protected float minAttackRange;
        
        [SerializeField]
        protected float maxAttackRange;

        [SerializeField]
        protected float attackCooldown;

        public AttackName AttackName => attackName;
        public float AttackRange => attackRange;
        public float MinAttackRange => minAttackRange;
        public float MaxAttackRange => maxAttackRange;
        public float AttackDamage => damage;
        public float AttackCooldown => attackCooldown;

        public abstract void Attack(Enemy enemy);

        public abstract bool CheckDealDamage(Enemy enemy);
    }
}