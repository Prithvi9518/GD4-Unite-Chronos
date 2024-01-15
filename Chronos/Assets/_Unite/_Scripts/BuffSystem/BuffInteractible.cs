using Unite.Interactibles;
using UnityEngine;

namespace Unite.BuffSystem
{
    public class BuffInteractible : InteractibleObject
    {
        [SerializeField]
        private BuffScriptableObject buff;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            buff.ApplyBuff(player);
            HandleInteraction();
        }
    }
}