using Unite.EventSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class PickupInteractible : InteractibleObject
    {

            [SerializeField]
            private TransformEvent pickupGameEvent;
            public override void HandleInteraction()
            {
                base.HandleInteraction();
                pickupGameEvent.Raise(transform);
            }
    }
}