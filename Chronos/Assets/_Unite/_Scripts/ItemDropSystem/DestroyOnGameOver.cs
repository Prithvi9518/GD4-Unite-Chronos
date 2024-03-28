using Unite.Managers;
using UnityEngine;

namespace Unite.ItemDropSystem
{
    public class DestroyOnGameOver : MonoBehaviour
    {
        private void OnEnable()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.OnGameLose += OnGameOver;
        }

        private void OnDisable()
        {
            if (GameManager.Instance == null) return;
            GameManager.Instance.OnGameLose -= OnGameOver;
        }

        private void OnGameOver() 
        {
            Destroy(gameObject);
        }
    }
}