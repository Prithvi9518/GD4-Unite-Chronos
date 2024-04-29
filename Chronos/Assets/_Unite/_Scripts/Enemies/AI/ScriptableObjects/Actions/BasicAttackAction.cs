using System.Collections.Generic;
using Unite.SoundScripts;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    /// <summary>
    /// Attack action used by the enemy state machine to perform an attack.
    /// Uses the AttackData ScriptableObject to get the concrete implementation of the attack.
    /// </summary>
    [CreateAssetMenu(fileName = "BasicAttack", menuName = "AI/Actions/Basic Attack")]
    public class BasicAttackAction : FSMAction
    {
        [SerializeField]
        private AttackData attackData;

        [SerializeField]
        private AudioClip attackSoundEffect;

        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            Attack attack = baseStateMachine.GetComponent<EnemyAttackHandler>().
                Attacks.GetValueOrDefault(attackData.name, null);

            if (!attack.CanUseAttack()) return;

            attack.DoAttack(baseStateMachine.GetComponent<Enemy>());

            if (attackSoundEffect == null) return;
            SoundEffectsManager.Instance.PlaySoundAtPosition(attackSoundEffect, baseStateMachine.transform.position);
        }
    }
}