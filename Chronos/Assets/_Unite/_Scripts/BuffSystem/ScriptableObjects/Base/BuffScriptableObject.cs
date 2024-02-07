using UnityEngine;

namespace Unite.BuffSystem
{
    public abstract class BuffScriptableObject : ScriptableObject
    {
        [SerializeField] 
        private string buffName;

        [SerializeField]
        private string description;

        public virtual void ApplyBuff(Player.Player player)
        {
            BuffsTrackingManager.Instance.AddBuffToTracker(this);
        }
    }
}