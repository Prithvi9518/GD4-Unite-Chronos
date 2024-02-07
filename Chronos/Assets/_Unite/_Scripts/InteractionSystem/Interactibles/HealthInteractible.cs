﻿using Unite.EventSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class HealthInteractible : InteractibleObject
    {
        [SerializeField]
        private float healthAmount;

        [SerializeField]
        private FloatEvent onHealthPickedUp;

        public override void HandleInteraction()
        {
            base.HandleInteraction();
            
            onHealthPickedUp.Raise(healthAmount);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            
            HandleInteraction();
        }
    }
}