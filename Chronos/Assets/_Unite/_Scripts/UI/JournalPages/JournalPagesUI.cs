using UnityEngine;

namespace Unite.UI
{
    public class JournalPagesUI : MonoBehaviour
    {
        [SerializeField]
        private Transform mainPanel;

        [SerializeField]
        private JournalPageSO[] journalPages;
        
        private void Awake()
        {
            mainPanel.gameObject.SetActive(false);
        }

        public void ShowJournalUI()
        {
            mainPanel.gameObject.SetActive(true);
        }

        public void HideJournalUI()
        {
            mainPanel.gameObject.SetActive(false);
        }
    }
}