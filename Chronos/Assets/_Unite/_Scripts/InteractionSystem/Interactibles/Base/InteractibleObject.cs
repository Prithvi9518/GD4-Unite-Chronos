using Unite.EventSystem;
using Unite.SoundScripts;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class InteractibleObject : MonoBehaviour, IProvideInteractText
    {
        [SerializeField] 
        private string displayName;
        
        [SerializeField]
        protected InteractibleSO interactibleData;
        
        [SerializeField]
        private InteractibleObjectEvent onInteractUpdateAnalytics;

        public string DisplayName => displayName;
        
        public virtual void HandleInteraction()
        {
            if (interactibleData == null) return;
            
            if(interactibleData.EventOnInteract != null)
                interactibleData.EventOnInteract.Raise();
            
            if(onInteractUpdateAnalytics != null)
                onInteractUpdateAnalytics.Raise(this);
            
            if (interactibleData.AudioToPlayOnInteract != null)
            {
                SoundEffectsManager.Instance.PlaySoundAtPosition(
                    interactibleData.AudioToPlayOnInteract,
                    transform.position
                );
            }
            
            if (!interactibleData.DestroyAfterInteract) return;
            Destroy(gameObject);
        }

        public string GetInteractText()
        {
            return interactibleData.InteractText;
        }
    }
}