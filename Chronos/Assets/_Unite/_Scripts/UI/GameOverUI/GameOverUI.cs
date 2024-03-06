using Unite.Managers;
using UnityEngine;

namespace Unite.UI
{
    public class GameOverUI : MonoBehaviour
    {
        public void OnClickRestartButton()
        {
            GameManager.Instance.HandleRestart();
        }

        public void OnClickExitButton()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}