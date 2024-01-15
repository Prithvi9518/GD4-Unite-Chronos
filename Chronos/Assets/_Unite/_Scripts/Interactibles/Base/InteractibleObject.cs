using Unite.EventSystem;
using Unite.SoundScripts;
using UnityEngine;

namespace Unite.Interactibles
{
    public class InteractibleObject : MonoBehaviour
    {
        [SerializeField] 
        private string displayName;
        
        [SerializeField]
        protected InteractibleSO interactibleData;

        [SerializeField]
        private InteractibleObjectEvent onInteractUpdateMetrics;

        public string DisplayName => displayName;
        
        public virtual void HandleInteraction()
        {
            if (interactibleData == null) return;
            
            if(interactibleData.EventOnInteract != null)
                interactibleData.EventOnInteract.Raise();
            
            if(onInteractUpdateMetrics != null)
                onInteractUpdateMetrics.Raise(this);
            
            if (interactibleData.AudioToPlayOnInteract != null)
            {
                SoundManager.Instance.PlaySoundAtPosition(
                    interactibleData.AudioToPlayOnInteract,
                    transform.position
                );
            }
            
            if (!interactibleData.DestroyAfterInteract) return;
            Destroy(gameObject);
        }
    }
}