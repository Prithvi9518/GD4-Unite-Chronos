using Unite.TimeStop;
using UnityEngine;


namespace Unite.SoundScripts
{
    public class SoundManager : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private SoundReferences soundReferences;

        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            if (!isTimeStopped) return;
            PlaySoundAtPosition(soundReferences.TimeStopSFX, Camera.main.transform.position);
        }

        public void PlaySoundAtCameraPosition(AudioClip audioClip, float volume = 1f)
        {
            PlaySoundAtPosition(audioClip, Camera.main.transform.position, volume);
        }

        private void PlaySoundAtPosition(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
    }
}

