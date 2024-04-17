using System.Collections.Generic;
using UnityEngine;

namespace Unite.DialogueSystem
{
    /// <summary>
    /// Maps dialogues to a certain DialogueTrigger enum.
    /// Used by the PlayerDialogueHandler to play dialogues according to what enum value is used.
    /// <seealso cref="PlayerDialogueHandler"/>
    /// </summary>
    [CreateAssetMenu(fileName = "DialogueTriggerMappingSO", menuName = "Dialogue System/Dialogue Trigger Mapping")]
    public class DialogueTriggerMappingSO : ScriptableObject
    {
        [SerializeField]
        private DialogueTrigger dialogueTrigger;

        [SerializeField]
        private List<DialogueSO> dialogues;

        public DialogueTrigger DialogueTrigger => dialogueTrigger;
        public List<DialogueSO> Dialogues => dialogues;
    }
}