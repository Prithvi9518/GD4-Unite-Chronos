using UnityEngine;

namespace Unite.InteractionSystem
{
    [CreateAssetMenu(fileName = "OutlineSelectionResponse", menuName = "Interaction/OutlineSelectionResponse")]
    public class OutlineSelectionResponse : SelectionResponse
    {
        public override void OnSelect(Transform transform)
        {
            if (!transform.TryGetComponent(out Outline outline))
            {
                outline = transform.gameObject.AddComponent<Outline>();
                outline.enabled = true;
                outline.OutlineColor = Color.yellow;
                outline.OutlineWidth = 10.0f;
            }
            else
            {
                outline.enabled = true;
            }
        }

        public override void OnDeselect(Transform transform)
        {
            if (!transform.TryGetComponent(out Outline outline)) return;
            outline.enabled = false;
        }
    }
}