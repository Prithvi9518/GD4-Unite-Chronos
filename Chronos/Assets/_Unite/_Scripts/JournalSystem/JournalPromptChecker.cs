using UnityEngine;

namespace Unite.JournalSystem
{
    public class JournalPromptChecker : MonoBehaviour
    {
        private JournalPageSO promptedJournalPage;

        public JournalPageSO PromptedJournalPage => promptedJournalPage;

        public void OnJournalPromptRaised(JournalPageSO journalPage)
        {
            promptedJournalPage = journalPage;
        }
        
        public void OnJournalPromptHidden()
        {
            promptedJournalPage = null;
        }
    }
}