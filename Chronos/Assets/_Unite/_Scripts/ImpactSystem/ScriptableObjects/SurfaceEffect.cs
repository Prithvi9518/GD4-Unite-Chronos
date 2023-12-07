using System.Collections.Generic;
using UnityEngine;

namespace Unite.ImpactSystem
{
    [CreateAssetMenu(fileName = "SurfaceImpactEffect", menuName = "Impact System/Surface Impact Effect")]
    public class SurfaceEffect : ScriptableObject
    {
        [SerializeField] private List<SpawnObjectImpactEffect> spawnObjectEffects;
        [SerializeField] private List<PlayAudioImpactEffect> playAudioEffects;

        public List<SpawnObjectImpactEffect> SpawnObjectEffects => spawnObjectEffects;
        public List<PlayAudioImpactEffect> PlayAudioEffects => playAudioEffects;
    }
}