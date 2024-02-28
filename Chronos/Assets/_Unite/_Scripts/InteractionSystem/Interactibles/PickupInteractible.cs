using Unite.EventSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class PickupInteractible : InteractibleObject
    {
            [SerializeField]
            private PickupEvent pickupGameEvent;
            
            [SerializeField]
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