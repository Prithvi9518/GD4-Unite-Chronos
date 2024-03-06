using Unite.ActionSystem;
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
        [Range(0,1)]
        private float volume = 1f;
        
        [SerializeField]
        private bool destroyAfterInteract;

        [SerializeField]
        private string interactText;

        [SerializeField]
        private bool showInteractText;

        [SerializeField] 
        private ActionContext[] actionsOnInteract;

        public GameEvent EventOnInteract => eventOnInteract;
        public bool DestroyAfterInteract => destroyAfterInteract;
        public AudioClip AudioToPlayOnInteract => audioToPlayOnInteract;
        public float ClipVolume => volume;
        public string InteractText => interactText;
        public bool ShowInteractText => showInteractText;
        public ActionContext[] ActionsOnInteract => actionsOnInteract;
    }
}