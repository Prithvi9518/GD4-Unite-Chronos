using Unite.TimeStop;
using UnityEngine;


namespace Unite.SoundScripts
{
    public class SoundManager : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private SoundReferences soundReferences;

        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            if (!isTimeStopped) return;
            if (cam == null) return;
            PlaySoundAtPosition(soundReferences.TimeStopSFX, cam.transform.position);
        }

        public void HandleBasicPistolShootEvent()
        {
            if (cam == null)
            {
                cam = Camera.main;
            }
            if (cam == null) return;
            
            PlaySoundAtPosition(soundReferences.PistolShootSFX, cam.transform.position);
        }

        private void PlaySoundAtPosition(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
    }
}

