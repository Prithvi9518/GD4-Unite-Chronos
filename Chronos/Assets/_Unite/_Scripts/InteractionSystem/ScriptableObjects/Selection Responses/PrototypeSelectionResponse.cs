using UnityEngine;

namespace Unite.InteractionSystem
{
    [CreateAssetMenu(fileName ="PrototypeSelectionResponse", menuName ="Interaction/PrototypeSelectionResponse")]
    public class PrototypeSelectionResponse : SelectionResponse
    {
        public override void OnSelect(Transform transform)
        {
            Debug.Log($"Selected {transform.name}");
        }

        public override void OnDeselect(Transform transform)
        {
            Debug.Log($"Deselected {transform.name}");
        }
    }
}

