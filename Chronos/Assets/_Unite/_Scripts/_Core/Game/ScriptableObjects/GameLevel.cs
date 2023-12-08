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

        public void LoadLevel()
        {
            foreach (var scene in scenes)
            {
                scene.LoadScene();
            }

            if (postProcessPrefab == null || defaultPostProcessProfile == null) return;
            instancePostProcessPrefab = Instantiate(postProcessPrefab);
            instancePostProcessPrefab.profile = defaultPostProcessProfile;
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
