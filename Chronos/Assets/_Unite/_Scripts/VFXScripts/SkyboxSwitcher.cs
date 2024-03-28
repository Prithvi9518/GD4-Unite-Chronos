using UnityEngine;

namespace Unite.VFXScripts
{
    public class SkyboxSwitcher : MonoBehaviour
    {
        [SerializeField]
        private Material roomSkyboxMat;
        
        [SerializeField]
        private Material islandSkyboxMat;

        public void SwitchToRoomSkybox()
        {
            if (roomSkyboxMat == null) return;
            RenderSettings.skybox = roomSkyboxMat;
            
            // Uncomment if using dynamic GI
            // DynamicGI.UpdateEnvironment();
        }

        public void SwitchToIslandSkybox()
        {
            if (islandSkyboxMat == null) return;
            RenderSettings.skybox = islandSkyboxMat;
            
            // Uncomment if using dynamic GI
            // DynamicGI.UpdateEnvironment();
        }
    }
}