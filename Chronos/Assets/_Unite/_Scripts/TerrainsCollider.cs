using TMPro;
using UnityEngine;

namespace _Unite._Scripts
{
    public class TerrainsCollider : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI uiText;

        private string currentTerrainName;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Terrain"))
            {
                string newTerrainName = other.gameObject.name;
            
                if (newTerrainName != currentTerrainName)
                {
                    uiText.text = other.gameObject.name;
                }
            }
        }
    }
}