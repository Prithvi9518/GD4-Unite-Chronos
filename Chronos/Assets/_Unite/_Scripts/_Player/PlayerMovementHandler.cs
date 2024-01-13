using StarterAssets;
using Unite.StatSystem;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerMovementHandler : MonoBehaviour
    {
        [SerializeField]
        private StatTypeSO speedStat;
        
        [SerializeField]
        private float sprintSpeedIncrement;
        
        private FirstPersonController controller;

        public FirstPersonController Controller => controller;

        private void Awake()
        {
            controller = GetComponent<FirstPersonController>();
        }

        public void UpdateSpeedValue(float newValue)
        {
            controller.MoveSpeed = newValue;
            controller.SprintSpeed = controller.MoveSpeed + sprintSpeedIncrement;
        }
    }
}