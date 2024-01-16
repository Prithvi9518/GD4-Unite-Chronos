using System.Collections.Generic;
using Unite.Enemies;
using UnityEngine;

namespace Unite.Managers
{
    public class AnalyticsManager : MonoBehaviour
    {
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