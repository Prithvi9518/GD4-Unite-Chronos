using System.Collections.Generic;
using Unite.Enemies;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Managers
{
    public class AnalyticsManager : MonoBehaviour
    {
        public void PlayerDied(PlayerDiedInfo info)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Death_Position_x", info.DeathPosition.x);
            data.Add("Death_Position_y", info.DeathPosition.y);
            data.Add("Death_Position_z", info.DeathPosition.z);
            data.Add("Killed_By_Enemy", info.KilledByAttacker);
            data.Add("Killed_By_Attack", info.KilledByAttack);
            
            // Send analytics event
        }
        public void EnemyDefeated(Enemy enemy)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Name", enemy.DisplayName);
            // Add more data..
            
            // Send analytics event
        }

        public void TimeStopUsed()
        {
            // Send analytics event
        }
    }
}