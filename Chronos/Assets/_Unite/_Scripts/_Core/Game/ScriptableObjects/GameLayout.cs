using System.Collections;
using System.Collections.Generic;
using Unite.StatePattern;
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

        [ReadOnly] [SerializeField] private int currentLevel;

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

        [ContextMenu("Load Layout")]
        public IEnumerator LoadLayout(Action onFinishLoadingLayout)
        {
            if (levels.Count == 0) yield break;
            List<AsyncOperation> scenesToLoad = levels[startLevel].LoadLevel();

            foreach (var sceneToLoad in scenesToLoad)
            {
                while (!sceneToLoad.isDone)
                {
                    yield return null;
                }
            }

            isStartLoaded = true;

            onFinishLoadingLayout?.Invoke();
        }

        [ContextMenu("Unload Layout")]
        public void UnloadLayout()
        {
            if (levels.Count == 0) return;

            levels[startLevel].UnloadLevel();

            isStartLoaded = false;
        }
    }
}
