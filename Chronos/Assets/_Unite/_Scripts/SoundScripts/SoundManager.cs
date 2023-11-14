using Unite.TimeStop;
using Unite.WeaponSystem;
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
            BasicPistol.OnBasicPistolShoot += PlayBasicPistolShootSound;
        }

        private void OnDisable()
        {
            if (TimeStopManager.Instance == null) return;
            BasicPistol.OnBasicPistolShoot -= PlayBasicPistolShootSound;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            if (isTimeStopped)
            {
                if (Camera.main == null) return;
                PlaySound(soundReferences.TimeStopSFX, Camera.main.transform.position);
            }
        }

        private void PlayBasicPistolShootSound()
        {
            if (cam == null)
            {
                Camera mainCam = Camera.main;
                if (mainCam == null)
                    return;
                else
                    cam = mainCam;
            }
            PlaySound(soundReferences.PistolShootSFX, cam.transform.position);
        }

        private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
    }
}

