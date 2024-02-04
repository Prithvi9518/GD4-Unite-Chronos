using Unite.Core.Input;
using Unite.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Unite.UI
{
    public class JournalPagesUI : MonoBehaviour
    {
        [SerializeField]
        private Transform mainPanel;

        [SerializeField]
        private Transform leftPanel;
        
        [SerializeField]
        private Transform middlePanel;
        
        [SerializeField]
        private Transform rightPanel;

        [SerializeField]
        private JournalPageSO[] journalPages;

        [SerializeField] 
        private GameEvent onJournalOpened;

        [SerializeField]
        private GameEvent onCloseButtonPressed;

        private Image leftImage;
        private Image middleImage;
        private Image rightImage;

        private int currentPageIndex;
        
        private void Awake()
        {
            leftImage = leftPanel.GetComponent<Image>();
            middleImage = middlePanel.GetComponent<Image>();
            rightImage = rightPanel.GetComponent<Image>();
            
            mainPanel.gameObject.SetActive(false);
        }

        public void ShowJournalUI()
        {
            InputManager.Instance.SwitchToJournalUIActionMap();
            
            mainPanel.gameObject.SetActive(true);
            ShowCurrentPage();
            onJournalOpened.Raise();
        }

        public void HideJournalUI()
        {
            InputManager.Instance.SwitchToDefaultActionMap();

            mainPanel.gameObject.SetActive(false);
            onCloseButtonPressed.Raise();
        }

        public void TryMovePageByOffset(int offset)
        {
            int index = currentPageIndex + offset;
            if (!PageIndexExists(index)) return;

            currentPageIndex = index;
            ShowCurrentPage();
        }

        private bool PageIndexExists(int index)
        {
            return index < journalPages.Length &&
                   index >= 0;
        }

        private void ShowCurrentPage()
        {
            JournalPageSO page = journalPages[currentPageIndex];
            if (!page.HasTwoPages)
            {
                leftPanel.gameObject.SetActive(false);
                rightPanel.gameObject.SetActive(false);
                middlePanel.gameObject.SetActive(true);
                
                middleImage.sprite = page.PageSprites[0];
            }
            else
            {
                leftPanel.gameObject.SetActive(true);
                rightPanel.gameObject.SetActive(true);
                middlePanel.gameObject.SetActive(false);

                leftImage.sprite = page.PageSprites[0];
                rightImage.sprite = page.PageSprites[1];
            }
        }
    }
}