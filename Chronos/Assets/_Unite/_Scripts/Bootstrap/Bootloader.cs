using System;
using Unite.Core.Game;
using Unite.Managers;
using UnityEngine;

namespace Unite.Bootstrap
{
    /// <summary>
    /// Handles loading/unloading of levels within a specified game layout
    ///
    /// <seealso cref="gameLayout"/>
    /// </summary>
    public class Bootloader : MonoBehaviour
    {
        public static Bootloader Instance { get; private set; }
        
        [SerializeField] [Tooltip("Contains the levels, scenes, etc. to load the game")]
        private GameLayout gameLayout;

        [SerializeField] [Tooltip("Spawns once to load core managers and persists between scenes")]
        private GameObject[] corePersistentPrefabs;

        private bool isLoaded;

        private Action onStartLoadingScene;
        private Action<float> onProgressLoadingScene;
        private Action onFinishLoadingScene;

        private int currentLevelIndex;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of Bootloader in the scene! Destroying current instance");
                Destroy(gameObject);
            }

            Instance = this;
        }

        private void Start()
        {
            DontDestroyOnLoad(this);

            if (corePersistentPrefabs == null)
                throw new ArgumentNullException($"persistent object prefabs have not been set in bootloader!");

            if (isLoaded) return;

            LoadPersistentObjectPrefabs();

            if(gameLayout.MainMenuScene != null)
                gameLayout.LoadMainMenu();
            else
                LoadStartLevelLayout();
        }

        public void LoadMainMenu()
        {
            if (gameLayout.MainMenuScene == null) return;
            UnloadCurrentLevel();
            gameLayout.LoadMainMenu();
        }

        public void UnloadMainMenu()
        {
            if (gameLayout.MainMenuScene == null) return;
            gameLayout.UnloadMainMenu();
        }

        public void LoadStartLevelLayout()
        {
            currentLevelIndex = gameLayout.StartLevelIndex;
            LoadCurrentLayout();

            isLoaded = true;
        }

        public void LoadNextLevel()
        {
            gameLayout.UnloadLayout(currentLevelIndex);
            currentLevelIndex++;
            LoadCurrentLayout();
        }

        public void UnloadCurrentLevel()
        {
            gameLayout.UnloadLayout(currentLevelIndex);
        }

        public void ReloadCurrentLevel()
        {
            LoadCurrentLayout();
        }

        private void LoadCurrentLayout()
        {
            StartCoroutine(gameLayout.LoadLayout(currentLevelIndex, onStartLoadingScene, onProgressLoadingScene, onFinishLoadingScene));
        }

        private void LoadPersistentObjectPrefabs()
        {
            foreach (var prefab in corePersistentPrefabs)
            {
                var instance = Instantiate(prefab);
                
                DontDestroyOnLoad(instance);
            }
        }

        private void HandleLevelLoadStart()
        {
            GameManager.Instance.OnStartLoadingLevel();
        }

        private void HandleLevelLoadProgress(float progress)
        {
            GameManager.Instance.OnProgressLoadingLevel(progress);
        }

        private void HandleLevelLoadFinish()
        {
            GameManager.Instance.OnFinishedLoadingLevel(gameLayout.GetLevelByIndex(currentLevelIndex));
        }

        private void OnEnable()
        {
            onStartLoadingScene += HandleLevelLoadStart;
            onProgressLoadingScene += HandleLevelLoadProgress;
            onFinishLoadingScene += HandleLevelLoadFinish;
        }

        private void OnDisable()
        {
            onStartLoadingScene -= HandleLevelLoadStart;
            onProgressLoadingScene -= HandleLevelLoadProgress;
            onFinishLoadingScene -= HandleLevelLoadFinish;
        }

        private void OnDestroy()
        {
            gameLayout.UnloadLayout(currentLevelIndex);
        }
    }
}