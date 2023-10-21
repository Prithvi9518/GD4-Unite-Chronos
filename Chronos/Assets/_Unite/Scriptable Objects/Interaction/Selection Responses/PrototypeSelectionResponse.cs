using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName ="PrototypeSelectionResponse", menuName ="Unite/Scriptable Objects/Interaction/PrototypeSelectionResponse")]
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

