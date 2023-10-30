using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        private float baseDamage;
        private Dictionary<AttackType, Attack> attackDictionary = new();

        private EnemyDetectionHandler detectionHandler;
        private ITakeDamage targetDamageable;

        public Dictionary<AttackType, Attack> Attacks => attackDictionary;

        private void Awake()
        {
            detectionHandler = GetComponent<EnemyDetectionHandler>();
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
                attackDictionary.Add(attackData.AttackType, new Attack(attackData));
            }
        }

        public void InvokeDamageDealtEvent(AttackData attackData)
        {
            Attack attackToUse = attackDictionary.GetValueOrDefault(attackData.AttackType, null);

            if (attackToUse == null) return;

            if (!attackToUse.IsTargetInAttackRange(detectionHandler)) return;

            float totalDamageDealt = baseDamage + attackToUse.AttackData.AttackDamage;

            if(targetDamageable == null)
            {
                targetDamageable = detectionHandler.Target.GetComponent<ITakeDamage>();
            }

            targetDamageable.TakeDamage(totalDamageDealt);
        }
    }
}