using UnityEngine;

namespace Unite.SoundScripts
{
    [CreateAssetMenu(fileName = "GunAudioConfig", menuName = "Sound/Gun Audio Config")]
    public class GunAudioConfig : ScriptableObject
    {
        [Range(0, 1f)] 
        [SerializeField] 
        private float volume;

        [SerializeField]
        private AudioClip[] fireClips;

        public void PlayShootingAudioClip()
        {
            SoundManager.Instance.PlaySoundAtCameraPosition(fireClips[Random.Range(0, fireClips.Length)], volume);
        }
    }
}