using System.Collections.Generic;
using UnityEngine;

namespace Unite.ImpactSystem
{
    [CreateAssetMenu(fileName = "PlayAudioEffect", menuName = "Impact System/Play Audio Effect")]
    public class PlayAudioImpactEffect : ScriptableObject
    {
        [SerializeField] private AudioSource audioSourcePrefab;
        [SerializeField] private List<AudioClip> audioClips;
        [Range(0f, 1f)]
        [SerializeField] private float minVolume;
        [Range(0f, 1f)]
        [SerializeField] private float maxVolume;

        public AudioSource AudioSourcePrefab => audioSourcePrefab;
        public List<AudioClip> AudioClips => audioClips;
        public float MinVolume => minVolume;
        public float MaxVolume => maxVolume;
    }
}