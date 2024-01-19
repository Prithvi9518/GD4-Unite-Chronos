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
            if (!transform.TryGetComponent(out IProvideInteractText textProvider)) return;
            onShowInteractText.Raise(textProvider.GetInteractText());
        }

        public override void OnDeselect(Transform transform)
        {
            if (!transform.TryGetComponent(out IProvideInteractText textProvider)) return;
            onHideInteractText.Raise();
        }
    }
}