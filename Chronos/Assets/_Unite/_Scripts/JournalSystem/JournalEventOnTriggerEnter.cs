using Unite.EventSystem;
using UnityEngine;

namespace Unite.JournalSystem
{
    public class JournalEventOnTriggerEnter : MonoBehaviour
    {
        [SerializeField]
        private JournalPageSO journalPage;
        
        [SerializeField]
        private JournalPageSOEvent journalEvent;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            
            journalEvent.Raise(journalPage);
        }
    }
}