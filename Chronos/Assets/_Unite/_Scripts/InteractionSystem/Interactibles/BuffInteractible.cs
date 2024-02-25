using Unite.BuffSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class BuffInteractible : InteractibleObject
    {
        [SerializeField]
        private BuffScriptableObject buff;

        public override void HandleInteraction()
        {
            buff.ApplyBuff(Managers.GameManager.Instance.Player);
            base.HandleInteraction();
        }
    }
}