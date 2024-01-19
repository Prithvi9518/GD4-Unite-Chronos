using UnityEngine;
using UnityEngine.Serialization;

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

        [Header("Next Dialogue Delay Settings")]
        [Header("Set delay to audio clip length + some offset:")]
        [SerializeField]
        private bool setDelayToClipLength;
        [SerializeField]
        private float delayOffset;
        
        [Header("Manually set the delay if needed:")]
        [SerializeField] 
        private float manualDelay;

        public string SpeakerName => speakerName;
        public string Text => text;
        public AudioClip Audio => audio;
        public float NextLineDelayInSeconds => (setDelayToClipLength) ? audio.length + delayOffset : manualDelay;
    }
}