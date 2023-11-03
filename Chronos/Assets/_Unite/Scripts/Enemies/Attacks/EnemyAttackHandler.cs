using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        private float baseDamage;
        private Dictionary<AttackName, Attack> attackDictionary = new();

        private EnemyStateMachine enemyStateMachine;
        private ITakeDamage targetDamageable;

        public Dictionary<AttackName, Attack> Attacks => attackDictionary;

        private void Awake()
        {
            enemyStateMachine = GetComponent<EnemyStateMachine>();
        }

        public void PerformSetup(float baseDamage, List<AttackData> attacks)
        {
            this.baseDamage = baseDamage;
            SetupAttackDict(attacks);
        }

        private void SetupAttackDict(List<AttackData> attacks)
        {
            attackDictionary.Clear();
            foreach (AttackData attackData in attacks)
            {
                attackDictionary.Add(attackData.AttackName, new Attack(attackData));
            }
        }

        public void CheckAndDealDamage(AttackName attackName)
        {
            Attack attackToUse = attackDictionary.GetValueOrDefault(attackName, null);

            if (attackToUse == null) return;
            if (!attackToUse.CheckDealDamage(enemyStateMachine)) return;

            float totalDamageDealt = baseDamage + attackToUse.AttackData.AttackDamage;

            if (targetDamageable == null)
            {
                targetDamageable = enemyStateMachine.DetectionHandler.Target.GetComponent<ITakeDamage>();
            }

            targetDamageable.TakeDamage(totalDamageDealt);
        }
    }
}