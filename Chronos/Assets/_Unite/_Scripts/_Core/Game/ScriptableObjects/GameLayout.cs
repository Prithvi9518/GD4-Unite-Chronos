using System;
using System.Collections;
using System.Collections.Generic;
using Unite.Team;
using Unity.Collections;
using UnityEngine;
using Action = System.Action;

namespace Unite.Core.Game
{
    [CreateAssetMenu(fileName = "GameLayout", menuName = "Game/Layout")]
    public class GameLayout : ScriptableObject
    {
        #region Title and Level Layout

        [SerializeField] private string title;

        [Header("Level Layout")]
        [SerializeField]
        private List<GameLevel> levels;

        [Min(0)]
        [Tooltip("Zero-based index in the list of level layouts for the start level in the game (e.g. 0)")]
        [SerializeField]
        private int startLevel;

        [ReadOnly]
        [SerializeField] private bool isStartLoaded;

        #endregion

        #region Menus

        [SerializeField] private GameScene mainMenu;
        [SerializeField] private GameScene pauseMenu;
        [SerializeField] private GameScene uiMenu;

        #endregion

        #region Development Team and Version

        [SerializeField] private string productOwner;
        [SerializeField] private string teamLead;
        [SerializeField] private string testLead;

        [SerializeField] private List<TeamMember> teamMembers;

        [Header("Version and Documentation (optional)")] 
        [SerializeField]
        private ProjectLifecycleStage stage;

        [Min(1)] [SerializeField] private float version;

        [SerializeField] private string repositoryURL;

        #endregion

        public int StartLevelIndex => startLevel;

        [ContextMenu("Load Layout")]
        public IEnumerator LoadLayout(int levelIndex, Action onStartLoading, Action<float> onProgressLoading, Action onFinishLoading)
        {
            if (levels.Count == 0) yield break;
            if(levelIndex < 0 || levelIndex >= levels.Count) yield break;
            
            onStartLoading?.Invoke();
            
            List<AsyncOperation> scenesToLoad = levels[levelIndex].LoadLevel();

            foreach (var sceneToLoad in scenesToLoad)
            {
                while (!sceneToLoad.isDone)
                {
                    float loadProgress = 0;

                    foreach (AsyncOperation operation in scenesToLoad)
                    {
                        loadProgress += operation.progress;
                    }

                    loadProgress /= scenesToLoad.Count;
                    onProgressLoading?.Invoke(loadProgress);

                    yield return null;
                }
            }

            if(levelIndex == startLevel)
                isStartLoaded = true;

            onFinishLoading?.Invoke();
        }

        [ContextMenu("Unload Layout")]
        public void UnloadLayout(int levelIndex)
        {
            if (levels.Count == 0) return;
            if(levelIndex < 0 || levelIndex >= levels.Count) return;

            levels[levelIndex].UnloadLevel();

            if(levelIndex == startLevel)
                isStartLoaded = false;
        }
    }
}
