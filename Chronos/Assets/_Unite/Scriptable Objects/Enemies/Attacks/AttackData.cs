using UnityEngine;

namespace Unite
{
    public abstract class AttackData : ScriptableObject
    {
        [SerializeField]
        protected float attackCooldown;

        [SerializeField]
        private float damage;

        protected float timeWhenLastAttacked;

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