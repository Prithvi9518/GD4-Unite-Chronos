using UnityEngine;

namespace Unite.ImpactSystem
{
    [System.Serializable]
    public class SurfaceType
    {
        [SerializeField] private Texture albedo;
        [SerializeField] private Surface surface;

        public Texture Albedo => albedo;
        public Surface Surface => surface;
    }
}