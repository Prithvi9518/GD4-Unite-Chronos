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

            private PickupInfo inspectItemInfo;

            private void Start()
            {
                inspectItemInfo = new PickupInfo(transform, zoomFactor);
            }

            public override void HandleInteraction()
            {
                base.HandleInteraction();
                pickupGameEvent.Raise(inspectItemInfo);
            }
    }


}