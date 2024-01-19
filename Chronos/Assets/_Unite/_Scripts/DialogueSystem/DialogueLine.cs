using UnityEngine;

namespace Unite.DialogueSystem
{
    [System.Serializable]
    public class DialogueLine
    {
        [SerializeField]
        private string speakerName;

        [SerializeField]
        [TextArea]
        private string text;

        [SerializeField]
        private AudioClip audio;

        [Header("Set a delay between lines if this line is part of a sequence:")]
        [SerializeField] 
        private float nextLineDelayInSeconds;

        public string SpeakerName => speakerName;
        public string Text => text;
        public AudioClip Audio => audio;
        public float NextLineDelayInSeconds => nextLineDelayInSeconds;
    }
}