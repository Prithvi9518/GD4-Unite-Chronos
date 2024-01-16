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

        public string DisplayName => displayName;
        
        public virtual void HandleInteraction()
        {
            if (interactibleData == null) return;
            
            if(interactibleData.EventOnInteract != null)
                interactibleData.EventOnInteract.Raise();
            
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
    }
}