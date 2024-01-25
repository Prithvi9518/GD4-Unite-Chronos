using UnityEngine;

namespace Unite.SoundScripts
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }
        
        [SerializeField]
        private AudioSource combatAudioSource;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one MusicManager in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
        }

        public void PlayCombatMusic()
        {
            combatAudioSource.Play();
        }

        public void StopCombatMusic()
        {
            combatAudioSource.Stop();
        }
    }
}