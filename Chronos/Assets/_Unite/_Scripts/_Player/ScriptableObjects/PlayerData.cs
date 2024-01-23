using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Base Stats Configuration")]
        [SerializeField]
        private BaseStats playerBaseStats;

        public void SetupPlayer(Player player)
        {
            player.StatsHandler.PerformSetup(playerBaseStats);
            player.HealthHandler.PerformSetup();
            player.MovementHandler.UpdateSpeedFromStats();
            player.GunHandler.PerformSetup(player.InputHandler, player.StatsHandler);
            player.StatusEffectable.PerformSetup(player);
        }
    }
}