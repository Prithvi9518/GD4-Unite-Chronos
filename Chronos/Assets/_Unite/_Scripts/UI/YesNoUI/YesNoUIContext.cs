using Unite.EventSystem;
using UnityEngine;

namespace Unite.UI
{
    [System.Serializable]
    public class YesNoUIContext
    {
        [field: SerializeField]
        public string HeaderText { get; private set; }
        
        [field: SerializeField]
        public GameEvent OnYes { get; private set; }
        
        [field: SerializeField]
        public GameEvent OnNo { get; private set; }
    }
}