using UnityEngine;

namespace Unite.SoundScripts
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }
        
        [SerializeField]
        private AudioSource combatAudioSource;
        
        [SerializeField]
        private AudioSource explorationAudioSource;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one MusicManager in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
        }

        private void Start()
        {
            combatAudioSource.Stop();
            explorationAudioSource.Play();
        }

        public void PlayCombatMusic()
        {
            combatAudioSource.Play();
        }

        public void StopCombatMusic()
        {
            combatAudioSource.Stop();
        }

        public void PlayExplorationMusic()
        {
            explorationAudioSource.Play();
        }
        
        public void StopExplorationMusic()
        {
            explorationAudioSource.Stop();
        }
    }
}