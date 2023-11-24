using System.Collections.Generic;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerStatsHandler : MonoBehaviour
    {
        private Dictionary<StatTypeSO, StatInfo> playerStats = new();

        public void PerformSetup(List<StatInfo> baseStats)
        {
            playerStats.Clear();
            foreach (var stat in baseStats)
            {
                playerStats.Add(stat.StatType, stat);
            }
        }

        public StatInfo GetStatInfo(StatTypeSO statType)
        {
            return playerStats.TryGetValue(statType, out var statInfo) ? statInfo : null;
        }
    }
}