using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Unite
{
    [CreateAssetMenu(fileName = "GameScene", menuName = "Unite/Scriptable Objects/Game/Scene")]
    public class GameScene : ScriptableObject
    {
        [Header("Scene Info")]
        [Tooltip("Ensure that the object provided is a valid Unity Scene")]
        [SerializeField]
        private Object sceneObject;

        public void LoadScene()
        {
            string sceneName = sceneObject.name;

            if (SceneManager.GetSceneByName(sceneName).isLoaded)
                return;

            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
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
