using UnityEngine;

namespace Unite.DialogueSystem
{
    [CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue System/Dialogue SO")]
    public class DialogueSO : ScriptableObject
    {
        [SerializeField]
        private string speakerName;

        [SerializeField]
        [TextArea]
        private string dialogueText;

        [SerializeField]
        private AudioClip dialogueAudio;

        [Header("Set a delay between dialogues if this dialogue is part of a sequence:")]
        [SerializeField] 
        private float nextDialogueDelayInSeconds;

        public string SpeakerName => speakerName;
        public string DialogueText => dialogueText;
        public AudioClip DialogueAudio => dialogueAudio;
        public float NextDialogueDelayInSeconds => nextDialogueDelayInSeconds;
    }
}