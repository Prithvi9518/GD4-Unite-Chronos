using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName ="BiteAttack", menuName ="Unite/Scriptable Objects/Enemies/Attacks/Bite")]
    public class BiteAttack : AttackData
    {
        public override bool CanUseAttack(EnemyStateMachine enemy)
        {
            return timeWhenLastAttacked + attackCooldown < Time.time;
        }

        public override void DoAttack(EnemyStateMachine enemy)
        {
            Debug.Log("Bite");
            timeWhenLastAttacked = Time.time;
        }
    }
}

