using UnityEngine;

namespace Unite.BuffSystem
{
    public abstract class BuffScriptableObject : ScriptableObject
    {
        [SerializeField] 
        private string buffName;

        [SerializeField]
        private string description;

        public abstract void ApplyBuff(Player.Player player);
    }
}