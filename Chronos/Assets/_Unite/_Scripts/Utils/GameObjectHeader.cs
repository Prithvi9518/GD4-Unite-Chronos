using UnityEngine;

namespace Unite.Utils
{
    // Ensure this class is in its own file or the script is adjusted to include it
    public class GameObjectHeader : MonoBehaviour
    {
        [SerializeField]
        private Color headerColor;
        
        public Color HeaderColor => headerColor;
    }
}