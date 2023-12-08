using UnityEngine;

namespace Unite.BuffSystem
{
    public class TestingBuffPickup : MonoBehaviour
    {
        [SerializeField]
        private BuffScriptableObject buff;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            buff.ApplyBuff(player);
            Destroy(gameObject);
        }
    }
}