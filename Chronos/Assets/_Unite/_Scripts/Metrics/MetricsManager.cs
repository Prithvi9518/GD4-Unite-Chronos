using System.Collections.Generic;
using Unite.Enemies;
using Unite.Interactibles;
using UnityEngine;

namespace Unite.Metrics
{
    public class MetricsManager : MonoBehaviour
    {
        private Dictionary<string, int> defeatedEnemiesDict = new();
        private Dictionary<string, int> interactedObjectsDict = new();

        public void UpdateDefeatedEnemies(Enemy enemy)
        {
            CheckAndIncrementValueInDictionary(defeatedEnemiesDict, enemy.DisplayName);
        }

        public void UpdateInteractedObjects(InteractibleObject interactible)
        {
            CheckAndIncrementValueInDictionary(interactedObjectsDict, interactible.DisplayName);

            foreach (var pair in interactedObjectsDict)
            {
                Debug.Log($"{pair.Key} : {pair.Value}");
            }
        }

        private void CheckAndIncrementValueInDictionary(Dictionary<string, int> dictionary, string key)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key]++;
            }
            else
            {
                dictionary[key] = 1;
            }
        }
    }
}