using System.Collections.Generic;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerStatsHandler
    {
        private Dictionary<StatTypeSO, Stat> playerStats = new();

        public void PerformSetup(BaseStats baseStats)
        {
            playerStats.Clear();
            foreach (var stat in baseStats.Stats)
            {
                playerStats.Add(stat.StatType, new Stat(stat.Value));
            }
        }

        public Stat GetStat(StatTypeSO statType)
        {
            return playerStats.TryGetValue(statType, out var statInfo) ? statInfo : null;
        }

        public void AddModifier(StatTypeSO statType, StatModifier modifier)
        {
            if (playerStats.TryGetValue(statType, out Stat stat))
            {
                stat.AddModifier(modifier);
            }
        }
    }
}