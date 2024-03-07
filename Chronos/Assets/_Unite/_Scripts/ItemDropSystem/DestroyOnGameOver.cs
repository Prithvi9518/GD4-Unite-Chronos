using Unite.Managers;
using UnityEngine;

namespace Unite.ItemDropSystem
{
    public class DestroyOnGameOver : MonoBehaviour
    {
        private void OnEnable()
        {
            GameManager.Instance.OnGameLose += OnGameOver;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnGameLose -= OnGameOver;
        }

        private void OnGameOver() 
        {
            Destroy(gameObject);
        }
    }
}