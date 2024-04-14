using System.Collections.Generic;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.BuffSystem
{
    /// <summary>
    /// Used to track the buffs that the player has acquired throughout the game.
    /// Raises an event whenever a new buff is added.
    /// </summary>
    public class BuffsTrackingManager : MonoBehaviour
    {
        public static BuffsTrackingManager Instance { get; private set; }

        [SerializeField]
        private GameEvent onUpdateBuffsTracked;
        
        private Dictionary<BuffScriptableObject, int> buffsDict = new();

        public Dictionary<BuffScriptableObject, int> BuffsDict => buffsDict;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of BuffsTrackingManager in the scene! Destroying current instance.");
                Destroy(this);
            }

            Instance = this;
        }

        public void AddBuffToTracker(BuffScriptableObject buff)
        {
            if (buffsDict.ContainsKey(buff))
                buffsDict[buff] += 1;
            else
                buffsDict.Add(buff, 1);

            onUpdateBuffsTracked.Raise();
        }
    }
}