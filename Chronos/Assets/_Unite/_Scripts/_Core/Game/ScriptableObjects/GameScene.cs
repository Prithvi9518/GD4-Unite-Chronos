using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unite.Core.Game
{
    /// <summary>
    /// Stores a Unity Scene object to load additively and asynchronously.
    /// <seealso cref="GameLevel"/>
    /// </summary>
    [CreateAssetMenu(fileName = "GameScene", menuName = "Game/Scene")]
    public class GameScene : ScriptableObject
    {
        [Header("Scene Info")]
        [Tooltip("Ensure that the object provided is a valid Unity Scene")]
        [SerializeField]
        private Object sceneObject;

        [SerializeField] 
        private string sceneName;

        public AsyncOperation LoadScene()
        {
            string sceneToLoad = GetSceneName();
            
            if (SceneManager.GetSceneByName(sceneToLoad).isLoaded)
                return null;

            return SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        }

        public void UnloadScene()
        {
            string sceneToLoad = GetSceneName();

            if (SceneManager.GetSceneByName(sceneToLoad).isLoaded)
            {
                SceneManager.UnloadSceneAsync(sceneToLoad);
            }
        }

        /// <summary>
        /// If in editor mode, use the scene object to get it's name.
        /// If in a build, use a string, as the scene object does not get serialized
        /// This gives a null reference exception.
        ///
        /// Until a solution is figured out to make
        /// scene objects serialize in build mode, this is a temporary fix.
        /// </summary>
        /// <returns></returns>
        private string GetSceneName()
        {
            string sceneToLoad;
            
#if UNITY_EDITOR
            sceneToLoad = sceneObject.name;
#else
            sceneToLoad = sceneName;
#endif

            return sceneToLoad;
        }
    }
}
