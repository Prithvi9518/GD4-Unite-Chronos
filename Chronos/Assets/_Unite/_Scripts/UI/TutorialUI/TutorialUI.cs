using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Unite.UI
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField]
        private Transform mainPanel;

        [SerializeField]
        private Transform arrow;

        [SerializeField]
        private float timeBeforeShowMs;

        [SerializeField]
        private float timeBeforeHideMs;

        [SerializeField] 
        private float arrowTweenYOffset;

        [SerializeField]
        private float arrowTweenCycleLength;

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

            arrow.DOMoveY(arrow.position.y + arrowTweenYOffset, arrowTweenCycleLength).SetLoops(-1, LoopType.Yoyo);

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