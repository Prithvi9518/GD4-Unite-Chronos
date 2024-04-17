using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class PromptFadeController : MonoBehaviour
    {
        [SerializeField] private Image[] images;
        [SerializeField] private TextMeshProUGUI[] texts;

        public void FadeOut(float fadeTimeInSeconds)
        {
            DoFade(0, fadeTimeInSeconds);
        }

        private void DoFade(float endValue, float fadeTimeInSeconds)
        {
            foreach (var image in images)
            {
                image.DOFade(endValue, fadeTimeInSeconds);
            }

            foreach (var text in texts)
            {
                text.DOFade(endValue, fadeTimeInSeconds);
            }
        }

        public void SetAlpha(float value)
        {
            Color tempColor;
            
            foreach (var image in images)
            {
                tempColor = image.color;
                tempColor.a = value;
                image.color = tempColor;
            }
            
            foreach (var text in texts)
            {
                tempColor = text.color;
                tempColor.a = value;
                text.color = tempColor;
            }
        }
    }
}