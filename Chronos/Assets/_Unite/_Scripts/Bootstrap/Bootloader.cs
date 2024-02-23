using System;
using Unite.Core.Game;
using Unite.Managers;
using UnityEngine;

namespace Unite.Bootstrap
{
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

        private int currentLevel;

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

            currentLevel = gameLayout.StartLevelIndex;
            LoadCurrentLayout();

            isLoaded = true;
        }

        public void LoadNextLevel()
        {
            gameLayout.UnloadLayout(currentLevel);
            currentLevel++;
            LoadCurrentLayout();
        }

        private void LoadCurrentLayout()
        {
            StartCoroutine(gameLayout.LoadLayout(currentLevel, onStartLoadingScene, onProgressLoadingScene, onFinishLoadingScene));
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
            GameManager.Instance.OnStartLoadingLevel(currentLevel);
        }

        private void HandleLevelLoadProgress(float progress)
        {
            GameManager.Instance.OnProgressLoadingLevel(progress);
        }

        private void HandleLevelLoadFinish()
        {
            GameManager.Instance.OnFinishedLoadingLevel(currentLevel);
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
            gameLayout.UnloadLayout(currentLevel);
        }
    }
}