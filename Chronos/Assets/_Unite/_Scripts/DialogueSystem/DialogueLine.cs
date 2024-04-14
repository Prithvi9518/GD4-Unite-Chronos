using UnityEngine;

namespace Unite.DialogueSystem
{
    /// <summary>
    /// Single line of dialogue. Makes up a part of a dialogue sequence within the DialogueSO.
    /// Contains information about the dialogue line and the speaker.
    /// <seealso cref="DialogueSO"/>
    /// </summary>
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

        [Header("Specify whether to show a subtitle for this line or not:")] 
        [SerializeField]
        private bool showSubtitles;

        public string SpeakerName => speakerName;
        public string Text => text;
        public AudioClip Audio => audio;
        public float NextLineDelayInSeconds => (setDelayToClipLength) ? audio.length + delayOffset : manualDelay;
        public bool ShowSubtitles => showSubtitles;
    }
}