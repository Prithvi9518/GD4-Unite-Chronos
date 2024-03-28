using System.Collections;
using Unite.EventSystem;
using UnityEngine;

namespace _Unite._Scripts
{
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