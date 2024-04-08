using UnityEngine;

namespace Unite.VFXScripts
{
    public class ForceFieldController : MonoBehaviour
    {
        private static readonly int DistortionStrength = Shader.PropertyToID("_DistortionStrength");

        [SerializeField]
        private MeshRenderer forceFieldMesh;

        [SerializeField] private Material innerMaterial;

        private Material material;

        private void Awake()
        {
            material = Instantiate(forceFieldMesh.sharedMaterial);
            forceFieldMesh.material = material;
        }

        public void StopDistortion()
        {
            // material.SetFloat(DistortionStrength, 0);
            forceFieldMesh.material = innerMaterial;
        }

        private void OnDestroy()
        {
            if(material != null)
                Destroy(material);
        }
    }
}