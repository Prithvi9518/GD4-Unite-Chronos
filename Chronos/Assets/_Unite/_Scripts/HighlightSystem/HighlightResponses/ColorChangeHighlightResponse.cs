using UnityEngine;

namespace Unite.HighlightSystem
{
    public class ColorChangeHighlightResponse : HighlightResponse
    {
        [SerializeField]
        private MeshRenderer meshRenderer;

        [SerializeField]
        private Color highlightColor;

        private Color initialColor;

        private Material material;

        private void Awake()
        {
            material = meshRenderer.material;
            initialColor = material.color;
        }

        public override void EnableHighlight()
        {
            material.color = highlightColor;
        }

        public override void DisableHighlight()
        {
            material.color = initialColor;
        }
    }
}