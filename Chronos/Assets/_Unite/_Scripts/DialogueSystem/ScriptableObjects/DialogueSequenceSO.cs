using System.Collections.Generic;
using UnityEngine;

namespace Unite.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueSequenceSO", menuName = "Dialogue System/Dialogue Sequence SO")]
    public class DialogueSequenceSO : ScriptableObject
    {
        [SerializeField]
        private List<DialogueSO> dialogues;

        public List<DialogueSO> Dialogues => dialogues;
    }
}