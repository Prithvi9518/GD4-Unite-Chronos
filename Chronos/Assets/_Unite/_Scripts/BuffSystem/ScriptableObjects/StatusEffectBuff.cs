using Unite.StatusEffectSystem;
using Unite.WeaponSystem.Modifiers;
using UnityEngine;

namespace Unite.BuffSystem
{
    [CreateAssetMenu(fileName = "StatusEffectBuff", menuName = "Buffs/Status Effect")]
    public class StatusEffectBuff : BuffScriptableObject
    {
        [SerializeField]
        private StatusEffectSO statusEffect;

        public override void ApplyBuff(Player.Player player)
        {
            StatusEffectModifier modifier = new StatusEffectModifier(statusEffect);
            player.GunHandler.ApplyModifier(modifier);
        }
    }
}