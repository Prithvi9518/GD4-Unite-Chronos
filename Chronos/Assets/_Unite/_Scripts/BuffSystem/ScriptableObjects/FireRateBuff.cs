using Unite.WeaponSystem.Modifiers;
using UnityEngine;

namespace Unite.BuffSystem
{
    [CreateAssetMenu(fileName = "FireRateBuff", menuName = "Buffs/Fire Rate")]
    public class FireRateBuff : BuffScriptableObject
    {
        [SerializeField]
        private float amount;
        
        /// <summary>
        /// Modifies the fire rate on the player's gun by the set amount
        /// </summary>
        public override void ApplyBuff(Player.Player player)
        {
            base.ApplyBuff(player);
            
            FireRateModifier modifier = new FireRateModifier(amount);
            player.GunHandler.ApplyModifier(modifier);
        }
    }
}