using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        private Dictionary<AttackType, Attack> attackDictionary = new();

        public Dictionary<AttackType, Attack> Attacks => attackDictionary;

        public void SetupAttackDict(List<AttackData> attacks)
        {
            attackDictionary.Clear();
            foreach (AttackData attackData in attacks)
            {
                attackDictionary.Add(attackData.AttackType, new Attack(attackData));
            }
        }
    }
}