using UnityEngine;

namespace Unite.BattleSystem
{
    /// <summary>
    /// Handles the enabling/disabling of a battle zone's barrier colliders
    /// </summary>
    public class BattleZoneBarrier : MonoBehaviour
    {
        [SerializeField]
        private Collider[] barrierColliders;

        private void Awake()
        {
            ToggleBarrierColliders(false);
        }

        public void ToggleBarrierColliders(bool toggle)
        {
            foreach (var barrierCollider in barrierColliders)
            {
                barrierCollider.enabled = toggle;
            }
        }
    }
}