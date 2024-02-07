using Unite.EventSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    [CreateAssetMenu(fileName = "InteractTextEventSelectionResponse", menuName = "Interaction/InteractTextEventSelectionResponse")]
    public class InteractTextEventSelectionResponse : SelectionResponse
    {
        [SerializeField]
        private StringEvent onShowInteractText;

        [SerializeField]
        private GameEvent onHideInteractText;
        
        public override void OnSelect(Transform transform)
        {
            if (!transform.TryGetComponent(out InteractibleObject interactible)) return;
            if (!interactible.ShowInteractText) return;
            onShowInteractText.Raise(interactible.GetInteractText());
        }

        public override void OnDeselect(Transform transform)
        {
            if (!transform.TryGetComponent(out InteractibleObject interactible)) return;
            onHideInteractText.Raise();
        }
    }
}