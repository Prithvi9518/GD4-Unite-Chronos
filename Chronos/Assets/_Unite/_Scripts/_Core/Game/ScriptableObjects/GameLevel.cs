using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Unite.Core.Game
{
    [CreateAssetMenu(fileName = "GameLevel", menuName = "Game/Level")]
    public class GameLevel : ScriptableObject
    {
        [Header("Scenes")]
        [SerializeField]
        private List<GameScene> scenes;

        [Header("Post-processing (optional)")] 
        [SerializeField]
        private bool useDefaultPostProcessingVolume;

        [SerializeField]
        private Volume postProcessPrefab;

        [SerializeField]
        private VolumeProfile defaultPostProcessProfile;

        private Volume instancePostProcessPrefab;

        public List<AsyncOperation> LoadLevel()
        {
            List<AsyncOperation> scenesToLoad = new();
            foreach (var scene in scenes)
            {
                AsyncOperation sceneToLoad = scene.LoadScene();
                if(sceneToLoad != null)
                    scenesToLoad.Add(sceneToLoad);
            }

            if (postProcessPrefab == null || defaultPostProcessProfile == null) return scenesToLoad;
            
            instancePostProcessPrefab = Instantiate(postProcessPrefab);
            instancePostProcessPrefab.profile = defaultPostProcessProfile;

            return scenesToLoad;
        }

        public void UnloadLevel()
        {
            foreach (var scene in scenes)
            {
                scene.UnloadScene();
            }

            if (instancePostProcessPrefab == null) return;
            Destroy(instancePostProcessPrefab);
        }
    }
}
