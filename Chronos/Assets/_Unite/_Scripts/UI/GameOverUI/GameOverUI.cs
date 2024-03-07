using System;
using Unite.Managers;
using UnityEngine;

namespace Unite.UI.GameOverUI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private Transform panel;

        private void OnEnable()
        {
            GameManager.Instance.OnGameRestart += HideGameOverScreen;
        }
        
        private void OnDisable()
        {
            GameManager.Instance.OnGameRestart -= HideGameOverScreen;
        }

        public void ShowGameOverScreen()
        {
            panel.gameObject.SetActive(true);
        }

        public void HideGameOverScreen()
        {
            panel.gameObject.SetActive(false);
        }
    }
}