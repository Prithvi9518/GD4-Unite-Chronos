using System.Collections.Generic;
using UnityEngine;

namespace Unite
{
    public class EnemyAttackHandler : MonoBehaviour
    {
        [SerializeField]
        private Dictionary<string, AttackData> attackDictionary;
    }
}

