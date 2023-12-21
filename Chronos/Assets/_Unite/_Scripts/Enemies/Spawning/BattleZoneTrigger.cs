using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class BattleZoneTrigger : MonoBehaviour
    {
        private BattleZone battleZone;

        private void Awake()
        {
            battleZone = GetComponent<BattleZone>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                battleZone.StartBattle();
            }
        }
    }
}

