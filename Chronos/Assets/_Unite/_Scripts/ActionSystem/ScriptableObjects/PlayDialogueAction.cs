using Unite.DialogueSystem;
using UnityEngine;

namespace Unite.ActionSystem
{
    [CreateAssetMenu(fileName = "PlayDialogueAction", menuName = "Action System/Play Dialogue Action")]
    public class PlayDialogueAction : ActionSO
    {
        [SerializeField] 
        private DialogueSO dialogueToPlay;
        
        public override void ExecuteAction()
        {
            DialogueManager.Instance.PlayDialogue(dialogueToPlay);
        }
    }
}