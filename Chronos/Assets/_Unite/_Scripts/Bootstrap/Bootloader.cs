using System;
using Unite.Core.Game;
using UnityEngine;

namespace Unite.Bootstrap
{
    public class Bootloader : MonoBehaviour
    {
        [SerializeField] [Tooltip("Contains the levels, scenes, etc. to load the game")]
        private GameLayout gameLayout;

        [SerializeField] [Tooltip("Spawns once to load core managers and persists between scenes")]
        private GameObject[] corePersistentPrefabs;

        private bool isLoaded;

        private Action onFinishLoadingScenes;

        private void Start()
        {
            DontDestroyOnLoad(this);

            if (corePersistentPrefabs == null)
                throw new ArgumentNullException($"persistent object prefabs have not been set in bootloader!");

            if (isLoaded) return;

            LoadPersistentObjectPrefabs();

            LoadGameLayout();

            isLoaded = true;
        }

        private void LoadGameLayout()
        {
            StartCoroutine(gameLayout.LoadLayout(onFinishLoadingScenes));
        }

        private void LoadPersistentObjectPrefabs()
        {
            foreach (var prefab in corePersistentPrefabs)
            {
                var instance = Instantiate(prefab);
                
                DontDestroyOnLoad(instance);
            }
        }

        private void StartBootstrap()
        {
            Bootstrapper.Instance.DoBootstrapAfterScenesLoaded();
        }

        private void OnEnable()
        {
            onFinishLoadingScenes += StartBootstrap;
        }

        private void OnDisable()
        {
            onFinishLoadingScenes -= StartBootstrap;
        }

        private void OnDestroy()
        {
            gameLayout.UnloadLayout();
        }
    }
}