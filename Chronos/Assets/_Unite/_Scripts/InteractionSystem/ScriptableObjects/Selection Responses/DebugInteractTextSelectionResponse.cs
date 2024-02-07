using UnityEngine;

namespace Unite.InteractionSystem
{
    [CreateAssetMenu(fileName = "DebugInteractTextSelectionResponse", menuName = "Interaction/DebugInteractTextSelectionResponse")]
    public class DebugInteractTextSelectionResponse : SelectionResponse
    {
        public override void OnSelect(Transform transform)
        {
            if (!transform.TryGetComponent(out InteractibleObject textProvider)) return;
            Debug.Log($"Selected {transform.name}. Interact Text: {textProvider.GetInteractText()}");
        }

        public override void OnDeselect(Transform transform)
        {
            Debug.Log($"Deselected {transform.name}");
        }
    }
}