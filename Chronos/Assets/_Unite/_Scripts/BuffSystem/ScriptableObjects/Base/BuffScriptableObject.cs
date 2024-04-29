using UnityEngine;

namespace Unite.BuffSystem
{
    /// <summary>
    /// Base ScriptableObject for a buff
    ///
    /// <seealso cref="StatBuff"/>
    /// <seealso cref="ExplosiveBulletsBuff"/>
    /// </summary>
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