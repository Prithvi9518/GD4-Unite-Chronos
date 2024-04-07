using System;
using DG.Tweening;
using TMPro;
using Unite.ObjectiveSystem;
using UnityEngine;

namespace Unite.UI
{
    public class ObjectiveTemplateUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI objectiveText;

        [Header("Color Tween Settings")] 
        [SerializeField]
        private bool enableTween;
        
        [SerializeField]
        private int numColorTweens;

        [SerializeField]
        private float tweenDurationInSeconds;

        [SerializeField] 
        private Color tweenColor;

        private Color originalColor;

        private void Awake()
        {
            originalColor = objectiveText.color;
        }

        public void UpdateObjectiveText(Objective objective)
        {
            objectiveText.text = objective.ObjectiveData.ObjectiveDescription;

            if (!enableTween) return;

            objectiveText.DOColor(tweenColor, tweenDurationInSeconds).SetLoops(numColorTweens, LoopType.Yoyo)
                .OnComplete(
                    () => objectiveText.DOColor(originalColor, tweenDurationInSeconds)
                );
        }
    }
}