using Unite.HighlightSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    [CreateAssetMenu(fileName = "HighlightableSelectionResponse", menuName = "Interaction/HighlightableSelectionResponse")]
    public class HighlightableSelectionResponse : SelectionResponse
    {
        public override void OnSelect(Transform transform)
        {
            // if (!transform.TryGetComponent(out Outline outline))
            // {
            //     outline = transform.gameObject.AddComponent<Outline>();
            //     outline.enabled = true;
            //     outline.OutlineColor = Color.yellow;
            //     outline.OutlineWidth = 10.0f;
            // }
            // else
            // {
            //     outline.enabled = true;
            // }

            if (!transform.TryGetComponent(out IHighlightable highlightable)) return;
            highlightable.EnableHighlight();
        }

        public override void OnDeselect(Transform transform)
        {
            // if (!transform.TryGetComponent(out Outline outline)) return;
            // outline.enabled = false;

            if (!transform.TryGetComponent(out IHighlightable highlightable)) return;
            highlightable.DisableHighlight();
        }
    }
}