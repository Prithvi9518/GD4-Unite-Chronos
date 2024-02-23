using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Unite.SoundScripts
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }

        [Header("FMOD Music Event Configuration")]
        [SerializeField]
        private string musicEventParameter;

        [SerializeField]
        [Range(0,1)]
        private float exploreParamValue;
        
        [SerializeField]
        [Range(0,1)]
        private float combatParamValue;
        
        [SerializeField]
        [Range(0,1)]
        private float explorationVolume;
        
        [SerializeField]
        [Range(0,1)]
        private float combatVolume;

        private EventInstance musicEventInstance;

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
            musicEventInstance = GetFMODEventInstance(FMODEvents.Instance.MusicTransitionEvent);
        }
        
        private EventInstance GetFMODEventInstance(EventReference eventReference)
        {
            return RuntimeManager.CreateInstance(eventReference);
        }

        public void StartPlayingMusic()
        {
            musicEventInstance.start();
            musicEventInstance.setVolume(explorationVolume);
        }

        public void PlayCombatMusic()
        {
            musicEventInstance.setVolume(combatVolume);
            musicEventInstance.setParameterByName(musicEventParameter, combatParamValue);
        }

        public void PlayExplorationMusic()
        {
            musicEventInstance.setVolume(explorationVolume);
            musicEventInstance.setParameterByName(musicEventParameter, exploreParamValue);
        }

        private void OnDestroy()
        {
            musicEventInstance.stop(STOP_MODE.IMMEDIATE);
            musicEventInstance.release();
        }
    }
}