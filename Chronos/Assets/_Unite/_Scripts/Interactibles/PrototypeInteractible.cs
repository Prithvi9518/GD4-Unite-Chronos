using Unite.Core.InteractionSystem;
using UnityEngine;

namespace Unite.Interactibles
{
    public class PrototypeInteractible : MonoBehaviour, IInteractible
    {
        public void Interact()
        {
            Debug.Log($"Interacting with {name}");
        }
    }
}


