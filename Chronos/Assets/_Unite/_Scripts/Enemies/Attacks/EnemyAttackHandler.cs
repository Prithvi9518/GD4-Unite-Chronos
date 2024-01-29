using System.Collections.Generic;
using Unite.Core.DamageInterfaces;
using UnityEngine;

namespace Unite.Enemies
{
    public class EnemyAttackHandler : MonoBehaviour, IAttacker
    {
        private float baseDamage;

        private Enemy enemy;
        private ITakeDamage targetDamageable;

        public Dictionary<string, Attack> Attacks { get; } = new();

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        public void PerformSetup(float baseDamage, List<AttackData> attacks)
        {
            this.baseDamage = baseDamage;
            SetupAttackDict(attacks);
        }

        private void SetupAttackDict(List<AttackData> attacks)
        {
            Attacks.Clear();
            foreach (AttackData attackData in attacks)
            {
                Attacks.Add(attackData.name, new Attack(attackData));
            }
        }

        public float GetTotalDamage(AttackData attack)
        {
            return baseDamage + attack.AttackDamage;
        }

        public void CheckAndDealDamage(AttackData attackData)
        {
            Attack attackToUse = Attacks.GetValueOrDefault(attackData.name, null);

            if (attackToUse == null) return;
            if (!attackToUse.CheckDealDamage(enemy)) return;

            float totalDamageDealt = GetTotalDamage(attackToUse.AttackData);

            if (targetDamageable == null)
            {
                targetDamageable = enemy.DetectionHandler.Target.GetComponent<ITakeDamage>();
            }

            targetDamageable.TakeDamage(totalDamageDealt, this, attackData);
        }

        public string GetName()
        {
            return enemy.DisplayName;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}