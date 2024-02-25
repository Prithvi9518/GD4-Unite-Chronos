using Unite.Managers;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerSpawnOnSceneLoad : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.RegisterPlayerSpawn(this);
        }

        public void SpawnPlayer(Player player)
        {
            if (player.TryGetComponent(out Rigidbody rb))
            {
                rb.position = transform.position;
            }
            else
            {
                player.transform.position = transform.position;
            }
        }
    }
}