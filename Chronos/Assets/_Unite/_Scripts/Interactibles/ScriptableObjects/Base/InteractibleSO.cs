using Unite.EventSystem;
using UnityEngine;

namespace Unite.Interactibles
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

        public GameEvent EventOnInteract => eventOnInteract;
        public bool DestroyAfterInteract => destroyAfterInteract;
        public AudioClip AudioToPlayOnInteract => audioToPlayOnInteract;
    }
}