using Unite.DialogueSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class TestDialogueInteractible : InteractibleObject
    {
        [SerializeField]
        private DialogueSequenceSO dialogueSequence;

        [SerializeField]
        private DialogueSO singleDialogue;

        [SerializeField]
        private bool isSequence;

        public override void HandleInteraction()
        {
            base.HandleInteraction();
            
            if(isSequence)
                DialogueManager.Instance.PlayDialogue(dialogueSequence);
            else
                DialogueManager.Instance.PlayDialogue(singleDialogue);
        }
    }
}