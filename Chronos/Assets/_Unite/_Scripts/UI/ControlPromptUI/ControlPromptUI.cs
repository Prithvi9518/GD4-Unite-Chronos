using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class ControlPromptUI : MonoBehaviour
    {
        [Header("Prompt Images:")]
        [SerializeField] private Image wasdImage;
        [SerializeField] private Image sprintImage;
        [SerializeField] private Image jumpImage;
        
        [Header("Fade Out Timing Variables:")]
        [SerializeField] private float timeBeforeFadeOutInSecs;
        [SerializeField] private float fadeOutTimeInSecs;
        
        public void ShowMovementPrompt()
        {
        }

        public void ShowSprintPrompt()
        {
        }

        public void ShowJumpPrompt()
        {
        }

        public void HideAllPrompts()
        {
        }
    }
}