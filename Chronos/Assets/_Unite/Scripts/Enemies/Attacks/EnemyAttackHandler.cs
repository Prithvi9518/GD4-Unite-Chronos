using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        private Dictionary<AttackType, AttackData> attackDictionary = new();

        public Dictionary<AttackType, AttackData> Attacks => attackDictionary;

        public void SetupAttackDict(List<AttackData> attacks)
        {
            attackDictionary.Clear();
            foreach (AttackData attack in attacks)
            {
                attackDictionary.Add(attack.AttackType, attack);
            }
        }
    }
}