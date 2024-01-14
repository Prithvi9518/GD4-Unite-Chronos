using Unite.Enemies.Movement;
using Unite.StatePattern;
using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.AI
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private Enemy enemy;

        public NavMeshAgent Agent => enemy.Agent;
        public Collider Collider => enemy.Collider;
        public EnemyDetectionHandler DetectionHandler => enemy.DetectionHandler;
        public EnemyAttackHandler AttackHandler => enemy.AttackHandler;
        public EnemyAnimationHandler AnimationHandler => enemy.AnimationHandler;
        public StrafeHandler StrafeHandler => enemy.StrafeHandler;

        protected override void Awake()
        {
            base.Awake();
            enemy = GetComponent<Enemy>();
        }
    }
}