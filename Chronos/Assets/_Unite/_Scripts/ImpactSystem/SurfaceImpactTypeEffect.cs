using UnityEngine;

namespace Unite.ImpactSystem
{
    [System.Serializable]
    public class SurfaceImpactTypeEffect
    {
        [SerializeField] private ImpactType impactType;
        [SerializeField] private SurfaceEffect surfaceEffect;

        public ImpactType ImpactType => impactType;
        public SurfaceEffect SurfaceEffect => surfaceEffect;
    }
}