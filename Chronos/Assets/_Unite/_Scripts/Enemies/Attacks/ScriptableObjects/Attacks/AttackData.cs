using Unite.Core.DamageInterfaces;
using UnityEngine;

namespace Unite.Enemies
{
    public abstract class AttackData : ScriptableObject, IDoDamage
    {
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

        public float AttackRange => attackRange;
        public float MinAttackRange => minAttackRange;
        public float MaxAttackRange => maxAttackRange;
        public float AttackDamage => damage;
        public float AttackCooldown => attackCooldown;

        public abstract void Attack(Enemy enemy);

        public abstract bool CheckDealDamage(Enemy enemy);

        public bool WithinAttackRange(Transform attacker, Transform target)
        {
            float distance = Vector3.Distance(attacker.transform.position, target.position);

            return distance <= maxAttackRange &&
                   distance >= minAttackRange;
        }

        public bool OutsideLowerRange(Transform attacker, Transform target)
        {
            float distance = Vector3.Distance(attacker.transform.position, target.position);

            return distance < minAttackRange;
        }
        
        public bool OutsideUpperRange(Transform attacker, Transform target)
        {
            float distance = Vector3.Distance(attacker.transform.position, target.position);

            return distance > maxAttackRange;
        }

        public string GetName()
        {
            return name;
        }
    }
}