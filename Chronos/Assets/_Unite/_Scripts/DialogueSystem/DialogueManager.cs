using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unite.DialogueSystem
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueManager : MonoBehaviour
    {
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
            StartCoroutine(DialogueLinesCoroutine(dialogue.Lines));
        }

        private void PlayDialogueLine(DialogueLine line)
        {
            Debug.Log($"{line.SpeakerName} : {line.Text}");
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