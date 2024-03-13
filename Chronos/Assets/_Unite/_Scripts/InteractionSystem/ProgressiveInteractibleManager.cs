using UnityEngine;

namespace Unite.InteractionSystem
{
    public class ProgressiveInteractibleManager : MonoBehaviour
    {
        public static ProgressiveInteractibleManager Instance { get; private set; }
        
        private ProgressiveInteractible currentInteractible;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }

        public void RegisterInteractible(ProgressiveInteractible interactible)
        {
            currentInteractible = interactible;
        }

        public void FinishProgressiveInteraction()
        {
            if (currentInteractible == null) return;
            
            Debug.Log($"ProgressiveInteractibleManager.{nameof(FinishProgressiveInteraction)}");
            
            if(currentInteractible.OnFinishInteraction != null)
                currentInteractible.OnFinishInteraction.Raise();
            
            currentInteractible = null;
        }
    }
}