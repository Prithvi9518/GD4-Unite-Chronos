using System.Collections.Generic;

namespace Unite
{
    public interface ISetupEnemy
    {
        public void SetupHealth(float maxHealth);

        public void SetupStateMachine(EnemyData enemyData);

        public void SetupAttacks(float baseDamage, List<AttackData> attacks);
    }
}