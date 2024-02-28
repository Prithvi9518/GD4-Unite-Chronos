using Unite.Managers;
using UnityEngine;

namespace Unite.Player
{
    public class PlayerSpawn : MonoBehaviour
    {
        [SerializeField]
        private Player playerPrefab;
        
        private void Start()
        {
            GameManager.Instance.RegisterPlayerSpawn(this);
        }

        public Player InstantiateAndSpawnPlayer()
        {
            if (playerPrefab == null)
            {
                Debug.LogError($"PlayerSpawn.{nameof(InstantiateAndSpawnPlayer)} - playerPrefab is null!");
                return null;
            }
            
            Player player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
            SpawnPlayer(player);

            return player;
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