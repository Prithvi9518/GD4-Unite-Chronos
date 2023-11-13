using UnityEngine;

namespace Unite
{
    public class PrototypeInteractible : MonoBehaviour, IInteractible
    {
        public void Interact()
        {
            Debug.Log($"Interacting with {name}");
        }
    }
}


