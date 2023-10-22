using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "PlayerWithinMeleeRangeCondition", menuName = "Unite/Scriptable Objects/AI/Conditions/PlayerWithinMeleeRange")]
    public class PlayerWithinMeleeRangeCondition : Condition
    {
        private EnemyStateMachine enemy;

        public override bool VerifyCondition(IStateMachine stateMachine)
        {
            if(enemy == null)
                enemy = stateMachine as EnemyStateMachine;

            return Vector3.Distance(enemy.transform.position,
                enemy.Target.transform.position) <= enemy.EnemyData.MeleeAttackRange;
        }
    }
}

