using System.Collections;
using TMPro;
using Unite.EventSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Unite._Scripts
{
    public class TerrainsCollider : MonoBehaviour
    {
        [SerializeField]
        private StringEvent uiTextEvent;
        private string currentTerrainName;
  
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Terrain"))
            {
                 string newTerrainName = other.gameObject.name;
            
                if (newTerrainName != currentTerrainName)
                {
                    uiTextEvent.Raise(other.gameObject.name);
                    StartCoroutine(FadeOutUIText());
                }
            }
        }

        private IEnumerator FadeOutUIText()
        {
            yield return new WaitForSeconds(5f);
            currentTerrainName = "";
            uiTextEvent.Raise(currentTerrainName);
        }
    }
}