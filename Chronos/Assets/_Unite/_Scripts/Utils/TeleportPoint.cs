using Unite.Managers;
using UnityEngine;

namespace Unite.Utils
{
    public class TeleportPoint : MonoBehaviour
    {
        [SerializeField] private KeyCode teleportKey;

        private Player.Player player;
        
        private void Update()
        {
            if (!Input.GetKeyDown(teleportKey)) return;
            
            if (player == null)
                player = GameManager.Instance.Player;
                
            TeleportPlayer();
        }

        private void TeleportPlayer()
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