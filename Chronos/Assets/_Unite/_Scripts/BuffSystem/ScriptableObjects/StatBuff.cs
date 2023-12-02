using Unite.Player;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.BuffSystem
{
    [CreateAssetMenu(fileName = "StatBuff", menuName = "Buffs/Stat Buff")]
    public class StatBuff : BuffScriptableObject
    {
        [SerializeField]
        private StatTypeSO statType;
        
        [SerializeField]
        private StatModifier modifier;
        
        public override void ApplyBuff(Player.Player player)
        {
            player.GetComponent<PlayerStatsHandler>().AddModifier(statType, modifier);
        }
    }
}