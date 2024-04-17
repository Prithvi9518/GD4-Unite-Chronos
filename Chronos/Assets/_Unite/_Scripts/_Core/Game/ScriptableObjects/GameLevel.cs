using System.Collections.Generic;
using Unite.ActionSystem;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Core.Game
{
    /// <summary>
    /// Models a single level in the game, containing multiple game scenes that are
    /// loaded additively and asynchronously when the level is loaded.
    ///
    /// Contains actions to perform when the level is loaded.
    ///
    /// <seealso cref="GameLayout"/>
    /// </summary>
    [CreateAssetMenu(fileName = "GameLevel", menuName = "Game/Level")]
    public class GameLevel : ScriptableObject
    {
        [Header("Scenes")]
        [SerializeField]
        private List<GameScene> scenes;

        [Header("Event to raise after loading the level:")]
        [SerializeField]
        private GameEvent onLoadLevel;

        [Header("Actions to execute after loading the level:")]
        [SerializeField]
        private ActionSO[] actionsOnLoad;
        
        public GameEvent OnLoadLevel => onLoadLevel;
        public ActionSO[] ActionsOnLoad => actionsOnLoad;

        public List<AsyncOperation> LoadLevel()
        {
            List<AsyncOperation> scenesToLoad = new();
            foreach (var scene in scenes)
            {
                AsyncOperation sceneToLoad = scene.LoadScene();
                if(sceneToLoad != null)
                    scenesToLoad.Add(sceneToLoad);
            }

            return scenesToLoad;
        }

        public void UnloadLevel()
        {
            foreach (var scene in scenes)
            {
                scene.UnloadScene();
            }
        }
    }
}
