using System.Collections;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Player
{
    /// <summary>
    /// Handles player collision with region triggers on the island.
    /// Raises an event with the region name (used to update the UI showing the region name)
    /// </summary>
    public class TerrainsCollider : MonoBehaviour
    {
        [SerializeField] private float displayDuration = 5f;
        [SerializeField] private StringEvent uiTextEvent;
        [SerializeField] private StringEvent onEnterRegionUpdateAnalytics;
        
        private string currentTerrainName;
  
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Terrain"))
            {
                 string newTerrainName = other.gameObject.name;
            
                if (newTerrainName != currentTerrainName)
                {
                    uiTextEvent.Raise(newTerrainName);
                    onEnterRegionUpdateAnalytics.Raise(newTerrainName);
                    StartCoroutine(FadeOutUIText());
                }
            }
        }

        private IEnumerator FadeOutUIText()
        {
            yield return new WaitForSeconds(displayDuration);
            currentTerrainName = "";
            uiTextEvent.Raise(currentTerrainName);
        }
    }
}