using Unite.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class LoadingScreenUI : MonoBehaviour
    {
        [SerializeField]
        private Transform panel;

        [SerializeField]
        private Image progressBar;

        private void Awake()
        {
            HideLoadingScreen();
        }

        private void OnEnable()
        {
            GameManager.Instance.OnProgressLevelLoad += UpdateProgress;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnProgressLevelLoad -= UpdateProgress;
        }

        public void ShowLoadingScreen()
        {
            panel.gameObject.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            progressBar.fillAmount = 0;
            panel.gameObject.SetActive(false);
        }

        private void UpdateProgress(float progress)
        {
            progressBar.fillAmount = progress;
        }
    }
}