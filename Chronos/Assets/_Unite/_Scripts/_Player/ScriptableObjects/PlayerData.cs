using System.Collections.Generic;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Unite/Scriptable Objects/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Base Stats Configuration")]
        [SerializeField]
        private List<StatInfo> playerBaseStats;

        public void SetupPlayer(Player player)
        {
            player.StatsHandler.PerformSetup(playerBaseStats);
        }
    }
}