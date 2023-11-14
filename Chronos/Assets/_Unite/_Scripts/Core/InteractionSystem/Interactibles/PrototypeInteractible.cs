using UnityEngine;

namespace Unite.Core.InteractionSystem
{
    public class PrototypeInteractible : MonoBehaviour, IInteractible
    {
        public void Interact()
        {
            Debug.Log($"Interacting with {name}");
        }
    }
}


