using Unite.ActionSystem;
using Unite.EventSystem;
using Unite.SoundScripts;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class InteractibleObject : MonoBehaviour
    {
        [SerializeField] 
        private string displayName;
        
        [SerializeField]
        protected InteractibleSO interactibleData;
        
        [SerializeField]
        private InteractibleObjectEvent onInteractUpdateAnalytics;

        public string DisplayName => displayName;
        public bool ShowInteractText => interactibleData.ShowInteractText;
        
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
                    transform.position,
                    interactibleData.ClipVolume
                );
            }

            foreach (var actionCtx in interactibleData.ActionsOnInteract)
            {
                if (actionCtx.DoOnce)
                {
                    if(actionCtx.ExecutedOnce) continue;
                    ActionExecutionManager.Instance.ExecuteAction(actionCtx.Action);
                    actionCtx.RegisterFirstExecution();
                }
                else
                {
                    ActionExecutionManager.Instance.ExecuteAction(actionCtx.Action);
                }
            }
            
            if (!interactibleData.DestroyAfterInteract) return;
            Destroy(gameObject);
        }
        
        public string GetInteractText()
        {
            return interactibleData == null ? string.Empty : interactibleData.InteractText;
        }
    }
}