using UnityEngine;

namespace Unite.UI
{
    public class YesNoUIContextSetter : MonoBehaviour
    {
        [SerializeField]
        private YesNoUI ui;
        
        [Header("For Exit Room:")] 
        [SerializeField]
        private YesNoUIContext exitRoomContext;

        public void SetExitRoomContext()
        {
            ui.SetContext(exitRoomContext);
        }
    }
}