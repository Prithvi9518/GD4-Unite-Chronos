using System.Collections.Generic;
using UnityEngine;

namespace Unite.ImpactSystem
{
    [CreateAssetMenu(fileName = "Surface", menuName = "Impact System/Surface")]
    public class Surface : ScriptableObject
    {
        [SerializeField] private List<SurfaceImpactTypeEffect> impactTypeEffects;

        public List<SurfaceImpactTypeEffect> ImpactTypeEffects => impactTypeEffects;
    }
}