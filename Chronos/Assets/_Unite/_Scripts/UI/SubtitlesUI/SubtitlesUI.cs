using System.Collections;
using TMPro;
using Unite.DialogueSystem;
using UnityEngine;

namespace Unite.UI
{
    public class SubtitlesUI : MonoBehaviour
    {
        [SerializeField]
        private Transform textBox;
        [SerializeField] 
        private TextMeshProUGUI subtitlesText;

        [SerializeField]
        private float disappearInSeconds;

        private Coroutine disappearCoroutine;
        
        private void Awake()
        {
            textBox.gameObject.SetActive(false);
        }

        public void ShowDialogueSubtitles(DialogueLine dialogueLine)
        {
            if (!dialogueLine.ShowSubtitles) return;
            
            if(disappearCoroutine != null)
                StopCoroutine(disappearCoroutine);
            
            string text = $"{dialogueLine.SpeakerName} : {dialogueLine.Text}";

            textBox.gameObject.SetActive(true);
            subtitlesText.text = text;

            disappearCoroutine = StartCoroutine(DisableSubtitleCoroutine());
        }

        private IEnumerator DisableSubtitleCoroutine()
        {
            yield return new WaitForSeconds(disappearInSeconds);
            textBox.gameObject.SetActive(false);
        }

        public void HideSubtitles()
        {
            textBox.gameObject.SetActive(false);
        }
    }
}