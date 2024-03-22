using UnityEngine;

namespace Unite.HighlightSystem
{
    public class DefaultHighlightable : MonoBehaviour, IHighlightable
    {
        [SerializeField]
        private HighlightResponse[] responses;

        private bool highlightEnabled;
        
        public void EnableHighlight()
        {
            if (highlightEnabled) return;
            
            foreach (var response in responses)
            {
                response.EnableHighlight();
            }

            highlightEnabled = true;
        }

        public void DisableHighlight()
        {
            if (!highlightEnabled) return;
            
            foreach (var response in responses)
            {
                response.DisableHighlight();
            }

            highlightEnabled = false;
        }
    }
}