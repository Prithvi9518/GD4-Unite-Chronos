using Unite.Notes;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class NoteInteractible : InteractibleObject
    {
        [SerializeField]
        private NoteSO noteData;
        public override void HandleInteraction()
        {
            base.HandleInteraction();
            
            Debug.Log($"{noteData.Title} : {noteData.Content}");
        }
    }
}