using UnityEngine;

namespace Unite.SoundScripts
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }
        
        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip explorationMusic;

        [SerializeField]
        private AudioClip combatMusic;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one MusicManager in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;

            audioSource.clip = explorationMusic;
        }

        private void Start()
        {
            // combatAudioSource.Stop();
            // explorationAudioSource.Play();
            
            audioSource.Play();
        }

        public void PlayCombatMusic()
        {
            // combatAudioSource.Play();
            audioSource.Stop();
            audioSource.clip = combatMusic;
            audioSource.Play();
        }

        public void StopCombatMusic()
        {
            // combatAudioSource.Stop();
        }

        public void PlayExplorationMusic()
        {
            // explorationAudioSource.Play();
            audioSource.Stop();
            audioSource.clip = explorationMusic;
            audioSource.Play();
        }
        
        public void StopExplorationMusic()
        {
            // explorationAudioSource.Stop();
        }
    }
}