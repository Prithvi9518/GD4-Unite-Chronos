using System;
using FMOD.Studio;
using FMODUnity;
using Unite.Managers;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Unite.SoundScripts
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager Instance { get; private set; }

        [Header("Room Soundtrack Audio Source")] 
        [SerializeField]
        private AudioSource roomSoundtrackAudioSource;

        [Header("Death Soundtrack Audio Source")] 
        [SerializeField]
        private AudioSource deathSoundtrackAudioSource;

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

        [SerializeField]
        private float timeStopPitch;
        
        [SerializeField]
        private float normalPitch;

        [SerializeField] 
        private float timeStopEQ;
        
        [SerializeField] 
        private float normalEQ;

        private EventInstance musicEventInstance;
        private PLAYBACK_STATE playbackState;

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
            musicEventInstance.getPlaybackState(out playbackState);
        }

        private void OnEnable()
        {
            GameManager.Instance.OnGameRestart += StopDeathSoundtrack;
        }
        
        private void OnDisable()
        {
            GameManager.Instance.OnGameRestart -= StopDeathSoundtrack;
        }

        private EventInstance GetFMODEventInstance(EventReference eventReference)
        {
            return RuntimeManager.CreateInstance(eventReference);
        }

        public void PlayRoomSoundtrack()
        {
            roomSoundtrackAudioSource.Play();
        }

        public void StopRoomSoundtrack()
        {
            roomSoundtrackAudioSource.Stop();
        }
        
        public void PlayDeathSoundtrack()
        {
            deathSoundtrackAudioSource.Play();
        }

        public void StopDeathSoundtrack()
        {
            deathSoundtrackAudioSource.Stop();
        }

        public void PlayCombatMusic()
        {
            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                musicEventInstance.start();
                musicEventInstance.getPlaybackState(out playbackState);
            }

            musicEventInstance.getPaused(out var paused);
            if (paused)
            {
                musicEventInstance.setPaused(false);
            }
            
            musicEventInstance.setVolume(combatVolume);
            musicEventInstance.setParameterByName(musicEventParameter, combatParamValue);
        }

        public void PlayExplorationMusic()
        {
            if (playbackState == PLAYBACK_STATE.STOPPED)
            {
                musicEventInstance.start();
                musicEventInstance.getPlaybackState(out playbackState);
            }
            
            musicEventInstance.setVolume(explorationVolume);
            musicEventInstance.setParameterByName(musicEventParameter, exploreParamValue);

            musicEventInstance.getPaused(out var paused);
            if (paused)
            {
                musicEventInstance.setPaused(false);
            }
        }

        public void PauseFMODEventInstance()
        {
            musicEventInstance.setPaused(true);
            musicEventInstance.getPlaybackState(out playbackState);
        }

        private void OnDestroy()
        {
            musicEventInstance.stop(STOP_MODE.IMMEDIATE);
            musicEventInstance.getPlaybackState(out playbackState);
            musicEventInstance.release();
        }
    }
}