using Unite.EventSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class PickupInteractible : ProgressiveInteractible
    {
            [SerializeField]
            private PickupEvent pickupGameEvent;
            
            [SerializeField]
            [Range(0f, 1f)]
            private float zoomFactor;
            
            [SerializeField]
            private bool disableRotation = false;

            private PickupInfo inspectItemInfo;

            private void Start()
            {
                inspectItemInfo = new PickupInfo(transform, zoomFactor, disableRotation);
            }

            public override void HandleInteraction()
            {
                base.HandleInteraction();
                pickupGameEvent.Raise(inspectItemInfo);
            }
    }


}