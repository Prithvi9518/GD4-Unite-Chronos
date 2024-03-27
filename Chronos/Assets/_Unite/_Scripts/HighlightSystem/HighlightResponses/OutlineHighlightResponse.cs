using UnityEngine;

namespace Unite.HighlightSystem
{
    public class OutlineHighlightResponse : HighlightResponse
    {
        [SerializeField]
        private Outline outline;

        [SerializeField]
        private Color outlineColor = Color.yellow;

        [SerializeField]
        private float outlineWidth = 10f;

        private void Start()
        {
            if (outline == null)
            {
                Debug.LogWarning($"{name}.{nameof(OutlineHighlightResponse)} - no outline component found. Adding component manually.");
                outline = gameObject.AddComponent<Outline>();
            }
            outline.OutlineColor = outlineColor;
            outline.OutlineWidth = outlineWidth;
            outline.enabled = false;
        }

        public override void EnableHighlight()
        {
            if (outline == null) return;
            outline.enabled = true;
        }

        public override void DisableHighlight()
        {
            if (outline == null) return;
            outline.enabled = false;
        }
    }
}