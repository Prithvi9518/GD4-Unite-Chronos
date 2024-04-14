using UnityEngine;

namespace Unite.BuffSystem
{
    /// <summary>
    /// Base class for selecting a buff
    ///
    /// <seealso cref="RandomBuffSelector"/>
    /// <seealso cref="WeightedRandomBuffSelector"/>
    /// </summary>
    public abstract class BuffSelector : MonoBehaviour
    {
        public abstract GameObject SelectBuff();
    }
}