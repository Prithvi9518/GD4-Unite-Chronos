using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName ="BasicMeleeAttack", menuName ="Unite/Scriptable Objects/AI/Actions/Basic Melee Attack")]
    public class BasicMeleeAttackAction : Action
    {
        public override void ExecuteAction(IStateMachine stateMachine)
        {
            Debug.Log("Melee Attack");
        }
    }
}

