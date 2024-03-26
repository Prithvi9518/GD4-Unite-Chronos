using UnityEngine;

namespace Unite.HighlightSystem
{
    public class PickupHighlightable : MonoBehaviour, IHighlightable
    {
        [SerializeField]
        private HighlightResponse[] responses;

        private bool highlightEnabled;
        private bool isBeingExamined;
        
        public void EnableHighlight()
        {
            if (isBeingExamined) return;
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

        public void HandleExamineStart()
        {
            isBeingExamined = true;
            DisableHighlight();
        }

        public void HandleExamineFinish()
        {
            isBeingExamined = false;
        }
    }
}