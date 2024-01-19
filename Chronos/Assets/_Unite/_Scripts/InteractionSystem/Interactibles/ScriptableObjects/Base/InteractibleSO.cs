using Unite.EventSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    [CreateAssetMenu(fileName = "InteractibleSO", menuName = "Interactibles/InteractibleSO")]
    public class InteractibleSO : ScriptableObject
    {
        [SerializeField]
        private GameEvent eventOnInteract;
        
        [SerializeField]
        private AudioClip audioToPlayOnInteract;
        
        [SerializeField]
        private bool destroyAfterInteract;

        [SerializeField]
        private string interactText;

        public GameEvent EventOnInteract => eventOnInteract;
        public bool DestroyAfterInteract => destroyAfterInteract;
        public AudioClip AudioToPlayOnInteract => audioToPlayOnInteract;
        public string InteractText => interactText;
    }
}