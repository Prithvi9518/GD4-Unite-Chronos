using StarterAssets;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField]
        private StatTypeSO speedStatType;
        
        [SerializeField]
        private float sprintSpeedIncrement;
        
        private FirstPersonController controller;
        private PlayerStatsHandler statsHandler;

        private void Awake()
        {
            controller = GetComponent<FirstPersonController>();
            statsHandler = GetComponent<PlayerStatsHandler>();
        }

        public void UpdateSpeedValue()
        {
            Stat speedStat = statsHandler.GetStat(speedStatType);
            if (speedStat == null) return;

            float baseSpeed = speedStat.Value;
            controller.MoveSpeed = baseSpeed;
            controller.SprintSpeed = baseSpeed + sprintSpeedIncrement;
        }
    }
}