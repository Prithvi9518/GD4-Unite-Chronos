﻿using System.Collections.Generic;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Health Configuration")] 
        [SerializeField]
        private float baseHealth;
        
        [Header("Player Base Stats Configuration")]
        [SerializeField]
        private BaseStats playerBaseStats;

        public void SetupPlayer(Player player)
        {
            player.HealthHandler.PerformSetup(baseHealth);
            player.StatsHandler.PerformSetup(playerBaseStats);
        }
    }
}