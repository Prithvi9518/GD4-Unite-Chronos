using System.Collections.Generic;
using UnityEngine;

namespace Unite.DialogueSystem
{
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