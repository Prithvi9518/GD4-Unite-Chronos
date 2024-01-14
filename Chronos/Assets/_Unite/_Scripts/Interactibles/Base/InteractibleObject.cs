using Unite.SoundScripts;
using UnityEngine;

namespace Unite.Interactibles
{
    public class InteractibleObject : MonoBehaviour
    {
        [SerializeField]
        protected InteractibleSO interactibleData;

        public virtual void HandleInteraction()
        {
            if (interactibleData == null) return;
            
            if(interactibleData.EventOnInteract != null)
                interactibleData.EventOnInteract.Raise();
            
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