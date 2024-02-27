using System.Collections;
using System.Collections.Generic;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.DialogueSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueManager : MonoBehaviour
    {
        [Header("Set to false for testing purposes:")]
        [SerializeField]
        private bool playDialogue = true;

        [Header("Send event to analytics manager when playing dialogue")]
        [SerializeField]
        private DialogueSOEvent dialogueAnalyticsEvent;
        
        public static DialogueManager Instance { get; private set; }

        private AudioSource audioSource;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one DialogueManager present in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayDialogue(DialogueSO dialogue)
        {
            if (!playDialogue) return;
            
            dialogueAnalyticsEvent.Raise(dialogue);
            StartCoroutine(DialogueLinesCoroutine(dialogue.Lines));
        }

        private void PlayDialogueLine(DialogueLine line)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(line.Audio);
        }

        private IEnumerator DialogueLinesCoroutine(List<DialogueLine> lines)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                bool hasNextDialogue = i < lines.Count - 1;
                
                PlayDialogueLine(lines[i]);
                
                if(!hasNextDialogue) continue;
                if(lines[i].NextLineDelayInSeconds == 0) continue;
                yield return new WaitForSeconds(lines[i].NextLineDelayInSeconds);
            }
        }
    }
}