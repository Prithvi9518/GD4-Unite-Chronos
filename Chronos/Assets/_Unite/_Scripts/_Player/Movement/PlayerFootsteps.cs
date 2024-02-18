using Unite.SoundScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.Player
{
    public class PlayerFootsteps : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] footstepSounds;

        [SerializeField]
        private float minTimeBetweenFootsteps;
        
        [SerializeField]
        private float maxTimeBetweenFootsteps;

        private float timeSinceLastFootstep;

        public void TryPlayFootstepSounds()
        {
            if (!(Time.time - timeSinceLastFootstep >=
                  Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))) return;
            
            // Play a random footstep sound from the array using SoundEffectsManager
            AudioClip footstepSound = footstepSounds[Random.Range(0, footstepSounds.Length)];
                
            float volume = 0.5f; // Adjust the volume as needed
            SoundEffectsManager.Instance.PlaySoundAtCameraPosition(footstepSound, volume);

            timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound
        }
    }
}