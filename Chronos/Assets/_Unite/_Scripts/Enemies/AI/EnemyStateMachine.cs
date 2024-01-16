using Unite.StatePattern;

namespace Unite.Enemies.AI
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private Enemy enemy;

        public EnemyDetectionHandler DetectionHandler => enemy.DetectionHandler;
        public EnemyAttackHandler AttackHandler => enemy.AttackHandler;

        protected override void Awake()
        {
            base.Awake();
            enemy = GetComponent<Enemy>();
        }
    }
}