using Unite.EventSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class ProgressiveInteractible : InteractibleObject
    {
        [SerializeField] private GameEvent onFinishInteraction;

        public GameEvent OnFinishInteraction => onFinishInteraction;

        public override void HandleInteraction()
        {
            base.HandleInteraction();
            ProgressiveInteractibleManager.Instance.RegisterInteractible(this);
        }
    }
}