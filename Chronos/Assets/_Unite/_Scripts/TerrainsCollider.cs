using TMPro;
using UnityEngine;

namespace _Unite._Scripts
{
    public class TerrainsCollider : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI uiText;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Terrain"))
            {
                uiText.text = other.name;
            }
        }
    }
}