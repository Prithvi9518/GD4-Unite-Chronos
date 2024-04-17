using Unite.StatePattern;

namespace Unite.Enemies.AI
{
    /// <summary>
    /// Finite state machine used by enemies to perform actions
    /// and check for transitions in each state.
    ///
    /// <seealso cref="BaseStateMachine"/>
    /// </summary>
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