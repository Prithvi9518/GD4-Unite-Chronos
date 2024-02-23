using System;
using Unite.Core.Game;
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

            LoadStartingLayout();

            isLoaded = true;
        }

        public void LoadNextLevel()
        {
            gameLayout.UnloadLayout(currentLevel);
            currentLevel++;
            StartCoroutine(gameLayout.LoadLayout(currentLevel, onFinishLoadingScene));
        }

        private void LoadStartingLayout()
        {
            currentLevel = gameLayout.StartLevelIndex;
            StartCoroutine(gameLayout.LoadLayout(currentLevel, onFinishLoadingScene));
        }

        private void LoadPersistentObjectPrefabs()
        {
            foreach (var prefab in corePersistentPrefabs)
            {
                var instance = Instantiate(prefab);
                
                DontDestroyOnLoad(instance);
            }
        }

        private void HandleLevelLoadFinish()
        {
            Managers.GameManager.Instance.OnFinishedLoadingLevel(currentLevel);
        }

        private void OnEnable()
        {
            onFinishLoadingScene += HandleLevelLoadFinish;
        }

        private void OnDisable()
        {
            onFinishLoadingScene -= HandleLevelLoadFinish;
        }

        private void OnDestroy()
        {
            gameLayout.UnloadLayout(currentLevel);
        }
    }
}