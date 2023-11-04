using UnityEngine;


namespace Unite
{
    public class PlayTimeStopSFX : MonoBehaviour, ITimeStopSubscriber
    {
        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            TimeStopManager.Instance.OnToggleTimeStop += HandleTimeStopEvent;
        }

        private void OnDisable()
        {
            if (TimeStopManager.Instance == null) return;
            TimeStopManager.Instance.OnToggleTimeStop -= HandleTimeStopEvent;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            if(isTimeStopped)
            {
                audioSource.Play();
            }
        }
    }
}

