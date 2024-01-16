using UnityEngine;

namespace Unite.Enemies.Spawning
{
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