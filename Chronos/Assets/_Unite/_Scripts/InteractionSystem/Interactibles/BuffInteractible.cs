using Unite.BuffSystem;
using UnityEngine;

namespace Unite.InteractionSystem
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