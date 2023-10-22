using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Unite/Scriptable Objects/Enemies/Base Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("Attack Configuration")]
        [SerializeField]
        public float MeleeAttackRange;

        [SerializeField]
        public float MeleeAttackDamage;
    }
}