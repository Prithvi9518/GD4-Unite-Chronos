using Unite.WeaponSystem.Modifiers;
using UnityEngine;

namespace Unite.BuffSystem
{
    [CreateAssetMenu(fileName = "FireRateBuff", menuName = "Buffs/Fire Rate")]
    public class FireRateBuff : BuffScriptableObject
    {
        [SerializeField]
        private float amount;
        public override void ApplyBuff(Player.Player player)
        {
            FireRateModifier modifier = new FireRateModifier(amount);
            player.GunHandler.ApplyModifier(modifier);
        }
    }
}