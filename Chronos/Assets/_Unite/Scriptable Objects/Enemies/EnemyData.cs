using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName ="EnemyData", menuName ="Unite/Scriptable Objects/Enemies/Base Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Melee Attack Configuration")]
        [SerializeField]
        public float MeleeAttackRange;
    }
}

