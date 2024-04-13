using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    /// <summary>
    /// Stores player stats and handles player setup.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Base Stats Configuration")]
        [SerializeField]
        private BaseStats playerBaseStats;

        public void SetupPlayer(Player player)
        {
            player.StatsHandler?.PerformSetup(playerBaseStats);

            if (player.HealthHandler == null)
            {
                Debug.LogWarning("PlayerData.SetupPlayer() - player.HealthHandler is null");
            }
            else
                player.HealthHandler.PerformSetup(player.StatsHandler);
            
            player.MovementHandler?.PerformSetup(player.StatsHandler);

            if (player.GunHandler == null)
            {
                Debug.LogWarning("PlayerData.SetupPlayer() - player.GunHandler is null");
            }
            else
                player.GunHandler.PerformSetup(player.StatsHandler);
            
            player.StatusEffectable.PerformSetup(player);
        }
    }
}