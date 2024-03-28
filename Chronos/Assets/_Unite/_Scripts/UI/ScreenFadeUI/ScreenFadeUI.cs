using System;
using System.Collections;
using DG.Tweening;
using Unite.SceneTransition;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class ScreenFadeUI : MonoBehaviour
    {
        [SerializeField] private Image panel;

        [SerializeField]
        private float fadeInTimeInSeconds;
        
        [SerializeField]
        private float fadeOutTimeInSeconds;

        [SerializeField] 
        private float waitAfterFadeOutSeconds;

        private void Awake()
        {
            var tempColor = panel.color;
            tempColor.a = 0f;
            panel.color = tempColor;
        }

        private void OnEnable()
        {
            if (SceneTransitionManager.Instance == null) return;

            SceneTransitionManager.Instance.OnStartTransition += HandleTransitionStart;
            SceneTransitionManager.Instance.OnFinishTransition += HandleTransitionFinish;
        }

        private void OnDisable()
        {
            if (SceneTransitionManager.Instance == null) return;
            
            SceneTransitionManager.Instance.OnStartTransition -= HandleTransitionStart;
            SceneTransitionManager.Instance.OnFinishTransition -= HandleTransitionFinish;
        }

        private void HandleTransitionStart(Action callback)
        {
            StartCoroutine(FadeOutCoroutine(callback));
        }
        
        private void HandleTransitionFinish(Action callback)
        {
            StartCoroutine(FadeInCoroutine(callback));
        }

        private IEnumerator FadeInCoroutine(Action callback)
        {
            var tweener = panel.DOFade(0f, fadeInTimeInSeconds);
            yield return tweener.WaitForCompletion();
            yield return new WaitForSeconds(waitAfterFadeOutSeconds);
            callback?.Invoke();
        }

        private IEnumerator FadeOutCoroutine(Action callback)
        {
            var tweener = panel.DOFade(1f, fadeOutTimeInSeconds);
            yield return tweener.WaitForCompletion();
            callback?.Invoke();
        }
    }
}