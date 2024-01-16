using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class BattleZoneTrigger : MonoBehaviour
    {
        private BattleZone battleZone;
        private Collider collider;

        private bool triggered;

        private void Awake()
        {
            battleZone = GetComponent<BattleZone>();
            collider = GetComponent<Collider>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (triggered) return;
            if (battleZone.BattleState != BattleState.Idle) return;

            if (!other.TryGetComponent(out Player.Player player)) return;

            if (!collider.bounds.Contains(other.bounds.min) ||
                !collider.bounds.Contains(other.bounds.max)) return;
            
            triggered = true;
            battleZone.Barrier.ToggleBarrierColliders(true);
            battleZone.StartBattle(player);
        }
    }
}

