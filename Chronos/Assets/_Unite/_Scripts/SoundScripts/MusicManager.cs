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
            audioSource.Play();
        }

        public void PlayCombatMusic()
        {
            audioSource.Stop();
            audioSource.clip = combatMusic;
            audioSource.Play();
        }

        public void PlayExplorationMusic()
        {
            audioSource.Stop();
            audioSource.clip = explorationMusic;
            audioSource.Play();
        }
    }
}