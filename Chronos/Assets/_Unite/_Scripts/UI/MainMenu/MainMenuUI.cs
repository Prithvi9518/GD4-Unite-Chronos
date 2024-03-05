using Unite.Bootstrap;
using UnityEngine;

namespace Unite.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void OnClickStartButton()
        {
            Bootloader.Instance.UnloadMainMenu();
            Bootloader.Instance.LoadStartLevelLayout();
        }

        public void OnClickQuitButton()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}