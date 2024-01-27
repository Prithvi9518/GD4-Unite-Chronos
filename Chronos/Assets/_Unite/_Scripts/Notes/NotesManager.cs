using System;
using System.Collections.Generic;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.Notes
{
    public class NotesManager : MonoBehaviour
    {
        public static NotesManager Instance { get; private set; }
        
        [SerializeField]
        private GameEvent onUpdateCollectedNotes;
        
        private List<NoteSO> collectedNotes = new();

        public List<NoteSO> CollectedNotes => collectedNotes;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one NotesManager in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
        }

        public void AddNote(NoteSO note)
        {
            collectedNotes.Add(note);
            onUpdateCollectedNotes.Raise();
            Debug.Log($"New note added to collection : {note.Title}");
        }
    }
}