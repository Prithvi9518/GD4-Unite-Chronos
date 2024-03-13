using Unite.SoundScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.Player
{
    public class PlayerFootsteps : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] grassFootSteps;

        [SerializeField]
        private AudioClip[] woodenFootSteps;
        
        [SerializeField]
        private float minTimeBetweenFootsteps;
        
        [SerializeField]
        private float maxTimeBetweenFootsteps;

        private float timeSinceLastFootstep;

        private bool playFootsteps;

        private AudioClip[] currentFootsteps;

        private void Awake()
        {
            currentFootsteps = grassFootSteps;
        }

        public void ToggleFootstepSounds(bool toggle)
        {
            playFootsteps = toggle;
        }

        public void TryPlayFootstepSounds()
        {
            if (!playFootsteps) return;
            if (!(Time.time - timeSinceLastFootstep >=
                  Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))) return;
            
            // Play a random footstep sound from the array using SoundEffectsManager
            AudioClip footstepSound = currentFootsteps[Random.Range(0, currentFootsteps.Length)];
                
            float volume = 0.5f; // Adjust the volume as needed
            SoundEffectsManager.Instance.PlaySoundAtCameraPosition(footstepSound, volume);

            timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound
        }

        public void SwitchToGrassFootsteps() 
        {
            currentFootsteps = grassFootSteps;
        }

        public void SwitchToWoodFootsteps()
        {
            currentFootsteps = woodenFootSteps;
        }
    }
}