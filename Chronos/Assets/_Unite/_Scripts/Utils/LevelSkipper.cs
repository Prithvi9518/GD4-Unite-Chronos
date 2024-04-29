using Unite.Managers;
using UnityEngine;

namespace Unite.Utils
{
    public class LevelSkipper : MonoBehaviour
    {
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.N))
                GameManager.Instance.SkipToNextLevel();
        }
    }
}