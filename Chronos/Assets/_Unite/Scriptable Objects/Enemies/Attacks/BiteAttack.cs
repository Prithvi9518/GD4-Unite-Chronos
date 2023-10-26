using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "BiteAttack", menuName = "Unite/Scriptable Objects/Enemies/Attacks/Bite")]
    public class BiteAttack : AttackData
    {
        public override void Attack(EnemyStateMachine enemy)
        {
            Debug.Log("Bite");
        }
    }
}