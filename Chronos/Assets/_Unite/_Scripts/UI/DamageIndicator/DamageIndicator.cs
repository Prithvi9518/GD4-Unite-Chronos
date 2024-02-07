using System.Collections;
using UnityEngine;

namespace Unite.UI
{
    public class DamageIndicator : MonoBehaviour
    {
        [SerializeField]
        private float maxTimeVisibleInSeconds;

        [SerializeField]
        private float fadeInRate = 4f;

        [SerializeField]
        private float fadeOutRate = 2f;

        [SerializeField]
        private float countdownTickRate = 1f;

        private float timer;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        private Transform player;
        private Transform target;

        private IEnumerator countdownEnumerator;
        private System.Action unregisterAction;
        
        private Quaternion targetRotation = Quaternion.identity;
        private Vector3 targetPosition = Vector3.zero;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        public void Register(Transform target, Transform player, System.Action unregister)
        {
            this.target = target;
            this.player = player;
            this.unregisterAction = unregister;

            StartCoroutine(RotateTowardsTarget());
            timer = maxTimeVisibleInSeconds;
            StartTimer();
        }

        public void Restart()
        {
            timer = maxTimeVisibleInSeconds;
            StartTimer();
        }

        private void StartTimer()
        {
            if (countdownEnumerator != null)
            {
                StopCoroutine(countdownEnumerator);
            }

            countdownEnumerator = Countdown();
            StartCoroutine(countdownEnumerator);
        }

        private IEnumerator Countdown()
        {
            while (canvasGroup.alpha < 1f)
            {
                canvasGroup.alpha += fadeInRate * Time.deltaTime;
                yield return null;
            }
            while (timer > 0)
            {
                timer--;
                yield return new WaitForSeconds(countdownTickRate);
            }
            while (canvasGroup.alpha > 0f)
            {
                canvasGroup.alpha -= fadeOutRate * Time.deltaTime;
                yield return null;
            }

            unregisterAction();
            Destroy(gameObject);
        }

        private IEnumerator RotateTowardsTarget()
        {
            while (enabled)
            {
                if (target)
                {
                    targetPosition = target.position;
                    targetRotation = target.rotation;
                }

                Vector3 direction = player.position - targetPosition;

                targetRotation = Quaternion.LookRotation(direction);
                targetRotation.z = -targetRotation.y;
                targetRotation.x = 0;
                targetRotation.y = 0;

                Vector3 northDirection = new Vector3(0, 0, player.eulerAngles.y);
                rectTransform.localRotation = targetRotation * Quaternion.Euler(northDirection);

                yield return null;
            }
        }
    }
}