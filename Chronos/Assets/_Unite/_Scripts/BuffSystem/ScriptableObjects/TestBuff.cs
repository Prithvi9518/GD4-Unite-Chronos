using UnityEngine;

namespace Unite.BuffSystem
{
    [CreateAssetMenu(fileName = "TestBuff", menuName = "Buffs/Test")]
    public class TestBuff : BuffScriptableObject
    {
        public override void ApplyBuff(Player.Player player)
        {
            Debug.Log("Test Buff");
        }
    }
}