using UnityEngine;

namespace Unite.BuffSystem
{
    public class TestBuffSelector : BuffSelector
    {
        [SerializeField]
        private GameObject buff;
        
        public override GameObject SelectBuff()
        {
            return buff;
        }
    }
}