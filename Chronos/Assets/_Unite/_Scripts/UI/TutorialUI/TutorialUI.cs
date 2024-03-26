using System.Collections;
using UnityEngine;

namespace Unite.UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField]
        private Transform mainPanel;

        [SerializeField]
        private float timeBeforeShowMs;

        [SerializeField]
        private float timeBeforeHideMs;

        private bool isDisplayed;
        private Coroutine showCoroutine;
        private Coroutine hideCoroutine;

        private void Awake()
        {
            mainPanel.gameObject.SetActive(false);
        }

        public void ShowTutorial()
        {
            if (isDisplayed) return;
            
            if(showCoroutine != null)
                StopCoroutine(showCoroutine);
            if(hideCoroutine != null)
                StopCoroutine(hideCoroutine);
            
            showCoroutine = StartCoroutine(ShowCoroutine());
        }

        public void HideTutorial()
        {
            if (!isDisplayed) return;
            isDisplayed = false;
            
            mainPanel.gameObject.SetActive(false);
            
            if (hideCoroutine == null) return;
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        private IEnumerator ShowCoroutine()
        {
            yield return new WaitForSeconds(timeBeforeShowMs);
            
            isDisplayed = true;
            mainPanel.gameObject.SetActive(true);

            if (hideCoroutine != null)
                StopCoroutine(hideCoroutine);
            hideCoroutine = StartCoroutine(HideCoroutine());
        }

        private IEnumerator HideCoroutine()
        {
            yield return new WaitForSeconds(timeBeforeHideMs);
            
            isDisplayed = false;
            mainPanel.gameObject.SetActive(false);
        }
    }
}