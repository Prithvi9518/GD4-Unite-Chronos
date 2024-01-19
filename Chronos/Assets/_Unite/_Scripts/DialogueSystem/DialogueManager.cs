using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unite.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one DialogueManager present in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
        }

        public void PlayDialogue(DialogueSO dialogue)
        {
            StartCoroutine(DialogueLinesCoroutine(dialogue.Lines));
        }

        // public void PlayDialogue(DialogueSequenceSO dialogueSequence)
        // {
        //     StartCoroutine(DialogueSequenceCoroutine(dialogueSequence.Dialogues));
        // }

        private void PlayDialogueLine(DialogueLine line)
        {
            Debug.Log($"{line.SpeakerName} : {line.Text}");
        }

        // private IEnumerator DialogueSequenceCoroutine(List<DialogueSO> dialogues)
        // {
        //     for (int i = 0; i < dialogues.Count; i++)
        //     {
        //         bool hasNextDialogue = i < dialogues.Count - 1;
        //         
        //         PlayDialogue(dialogues[i]);
        //         
        //         if(!hasNextDialogue) continue;
        //         yield return new WaitForSeconds(dialogues[i].NextDialogueDelayInSeconds);
        //     }
        // }
        
        private IEnumerator DialogueLinesCoroutine(List<DialogueLine> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                bool hasNextDialogue = i < lines.Count - 1;
                
                PlayDialogueLine(lines[i]);
                
                if(!hasNextDialogue) continue;
                yield return new WaitForSeconds(lines[i].NextLineDelayInSeconds);
            }
        }
    }
}