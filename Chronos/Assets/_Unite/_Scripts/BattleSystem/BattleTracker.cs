using Unite.ActionSystem;
using UnityEngine;

namespace Unite.BattleSystem
{
    public class BattleTracker : MonoBehaviour
    {
        public static BattleTracker Instance { get; private set; }

        [SerializeField] 
        private ActionSO[] onFirstBattleZoneActions;
        
        private BattleZone currentBattleZone;
        private int numBattleZonesEncountered;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }

        public void RegisterBattleZone(BattleZone zone)
        {
            currentBattleZone = zone;
            numBattleZonesEncountered++;
            CheckFirstBattleZone();
        }

        private void CheckFirstBattleZone()
        {
            if (numBattleZonesEncountered != 1) return;
            
            if (ActionExecutionManager.Instance == null) return;
            foreach (var action in onFirstBattleZoneActions)
            {
                ActionExecutionManager.Instance.ExecuteAction(action);
            }
        }
    }
}