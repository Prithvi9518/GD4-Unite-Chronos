using System.Collections;
using UnityEngine;

namespace Unite.UI
{
    public class ControlPromptUI : MonoBehaviour
    {
        [Header("Prompts:")]
        [SerializeField] private PromptFadeController movementPrompt;
        [SerializeField] private PromptFadeController sprintPrompt;
        [SerializeField] private PromptFadeController jumpPrompt;
        [SerializeField] private PromptFadeController dashPrompt;

        [Header("Fade Timing Variables:")] 
        [SerializeField] private float timeBeforeFadeOutInSecs;
        [SerializeField] private float fadeOutTimeInSecs;

        private Coroutine fadeOutCoroutine;

        private void Awake()
        {
            movementPrompt.gameObject.SetActive(false);
            sprintPrompt.gameObject.SetActive(false);
            jumpPrompt.gameObject.SetActive(false);
            dashPrompt.gameObject.SetActive(false);
        }

        public void ShowMovementPrompt()
        {
            TryShowPrompt(movementPrompt);
        }

        public void ShowSprintPrompt()
        {
            TryShowPrompt(sprintPrompt);
        }

        public void ShowJumpPrompt()
        {
            TryShowPrompt(jumpPrompt);
        }
        
        public void ShowDashPrompt()
        {
            TryShowPrompt(dashPrompt);
        }

        public void HideAllPrompts()
        {
        }

        private void TryShowPrompt(PromptFadeController prompt)
        {
            if (prompt == null) return;
            
            prompt.gameObject.SetActive(true);
            prompt.SetAlpha(1);
            fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(prompt));
        }

        private IEnumerator FadeOutCoroutine(PromptFadeController prompt)
        {
            yield return new WaitForSeconds(timeBeforeFadeOutInSecs);
            prompt.FadeOut(fadeOutTimeInSecs);
        }
    }
}