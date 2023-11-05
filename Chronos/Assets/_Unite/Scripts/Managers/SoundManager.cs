using UnityEngine;


namespace Unite
{
    public class SoundManager : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private SoundReferences soundReferences;

        private void Start()
        {
            TimeStopManager.Instance.OnToggleTimeStop += HandleTimeStopEvent;
            BasicPistol.OnBasicPistolShoot += PlayBasicPistolShootSound;
        }

        private void OnDisable()
        {
            if (TimeStopManager.Instance == null) return;
            TimeStopManager.Instance.OnToggleTimeStop -= HandleTimeStopEvent;
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
            if (Camera.main == null) return;
            PlaySound(soundReferences.PistolShootSFX, Camera.main.transform.position);
        }

        private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
        {
            AudioSource.PlayClipAtPoint(audioClip, position, volume);
        }
    }
}

