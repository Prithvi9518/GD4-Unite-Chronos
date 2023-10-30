using System.Collections.Generic;

namespace Unite
{
    public interface ISetupEnemy
    {
        public void SetupHealth(float maxHealth);

        public void SetupBaseDamage(float baseDamage);

        public void SetupStateMachine(EnemyData enemyData);

        public void SetupAttacks(List<AttackData> attacks);
    }
}