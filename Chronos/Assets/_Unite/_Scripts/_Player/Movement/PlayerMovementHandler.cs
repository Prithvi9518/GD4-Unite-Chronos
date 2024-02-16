using StarterAssets;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerMovementHandler : MonoBehaviour, IHandlePlayerMovement
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

        public void UpdateSpeedFromStats()
        {
            Stat speedStat = statsHandler.GetStat(speedStatType);
            if (speedStat == null) return;

            float baseSpeed = speedStat.Value;
            controller.MoveSpeed = baseSpeed;
            controller.SprintSpeed = baseSpeed + sprintSpeedIncrement;
        }

        public void ModifySpeed(float modifier)
        {
            controller.MoveSpeed += modifier;
            controller.SprintSpeed += modifier;
        }

        public void ToggleMovement(bool toggle)
        {
            controller.enabled = toggle;
        }
    }
}