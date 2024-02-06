using Unite.DialogueSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class DialogueInteractible : InteractibleObject
    {
        [SerializeField]
        private DialogueSO dialogue;

        public override void HandleInteraction()
        {
            base.HandleInteraction();
            
            DialogueManager.Instance.PlayDialogue(dialogue);
        }
    }
}