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
            HandleInteraction();
        }

        public override void HandleInteraction()
        {
            buff.ApplyBuff(Managers.GameManager.Instance.Player);
            base.HandleInteraction();
        }
    }
}