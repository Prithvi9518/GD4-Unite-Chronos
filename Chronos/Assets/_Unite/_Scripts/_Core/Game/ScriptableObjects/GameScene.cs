using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unite.Core.Game
{
    [CreateAssetMenu(fileName = "GameScene", menuName = "Game/Scene")]
    public class GameScene : ScriptableObject
    {
        [Header("Scene Info")]
        [Tooltip("Ensure that the object provided is a valid Unity Scene")]
        [SerializeField]
        private Object sceneObject;

        public AsyncOperation LoadScene()
        {
            string sceneName = sceneObject.name;

            if (SceneManager.GetSceneByName(sceneName).isLoaded)
                return null;

            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        public void UnloadScene()
        {
            string sceneName = sceneObject.name;

            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneName);
            }
        }
    }
}
