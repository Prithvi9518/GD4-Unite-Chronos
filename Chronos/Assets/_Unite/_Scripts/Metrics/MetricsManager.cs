using System.Collections.Generic;
using Unite.Enemies;
using UnityEngine;

namespace Unite.Metrics
{
    public class MetricsManager : MonoBehaviour
    {
        private Dictionary<string, int> defeatedEnemiesDict = new();

        public void UpdateDefeatedEnemies(Enemy enemy)
        {
            if (defeatedEnemiesDict.ContainsKey(enemy.DisplayName))
            {
                defeatedEnemiesDict[enemy.DisplayName]++;
            }
            else
            {
                defeatedEnemiesDict[enemy.DisplayName] = 1;
            }
        }
    }
}