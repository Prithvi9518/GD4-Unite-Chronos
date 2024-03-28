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

        [Header("Send event to subtitles UI when playing dialogue line")] 
        [SerializeField]
        private DialogueLineEvent onPlayDialogueLine;
        
        public static DialogueManager Instance { get; private set; }

        private AudioSource audioSource;

        private Coroutine dialogueLinesCoroutine;

        private bool isDialoguePlaying;
        
        private Queue<DialogueSO> dialogueQueue = new();

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

            if (isDialoguePlaying && dialogue.IsQueued)
            {
                Debug.Log($"Enqueueing dialogue - {dialogue.name}");
                dialogueQueue.Enqueue(dialogue);
                return;
            }
            
            Debug.Log($"Playing dialogue - {dialogue.name}");
            
            dialogueAnalyticsEvent.Raise(dialogue);
            
            if(dialogueLinesCoroutine != null)
                    StopCoroutine(dialogueLinesCoroutine);
            dialogueLinesCoroutine = StartCoroutine(DialogueLinesCoroutine(dialogue.Lines));
        }

        private void PlayDialogueLine(DialogueLine line)
        {
            if (line.Audio != null)
            {
                audioSource.Stop();
                audioSource.PlayOneShot(line.Audio);
            }
            
            onPlayDialogueLine.Raise(line);
        }

        private IEnumerator DialogueLinesCoroutine(List<DialogueLine> lines)
        {
            isDialoguePlaying = true;
            
            for (int i = 0; i < lines.Count; i++)
            {
                bool hasNextDialogue = i < lines.Count - 1;
                
                PlayDialogueLine(lines[i]);
                
                if(!hasNextDialogue) continue;
                if(lines[i].NextLineDelayInSeconds == 0) continue;
                yield return new WaitForSeconds(lines[i].NextLineDelayInSeconds);
            }

            isDialoguePlaying = false;
            if (dialogueQueue.Count <= 0) yield break;
            
            DialogueSO nextDialogue = dialogueQueue.Dequeue();
            Debug.Log($"Dequeued dialogue - {nextDialogue.name}");
            PlayDialogue(nextDialogue);
        }
    }
}