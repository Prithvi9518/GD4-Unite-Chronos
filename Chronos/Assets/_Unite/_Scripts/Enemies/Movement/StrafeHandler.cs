using UnityEngine;
using UnityEngine.AI;

namespace Unite.Enemies.Movement
{
    public class StrafeHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform strafeLeftTransform;

        [SerializeField]
        private Transform strafeRightTransform;

        private NavMeshAgent agent;

        private bool isStrafing;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!isStrafing) return;
            
            Debug.Log("strafing");
        }

        public void ToggleStrafing(bool active)
        {
            isStrafing = active;
        }
    }
}