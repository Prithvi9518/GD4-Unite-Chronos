using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.SoundScripts
{
    [CreateAssetMenu(fileName = "GunAudioConfig", menuName = "Sound/Gun Audio Config")]
    public class GunAudioConfig : ScriptableObject, ICloneable
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

        public object Clone()
        {
            GunAudioConfig clone = CreateInstance<GunAudioConfig>();
            clone.volume = volume;
            clone.fireClips = fireClips;

            return clone;
        }
    }
}