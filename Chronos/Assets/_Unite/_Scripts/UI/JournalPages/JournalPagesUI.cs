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
        private Transform prevButton;
        
        [SerializeField]
        private Transform nextButton;
        
        [SerializeField]
        private Transform closeButton;

        [SerializeField]
        private JournalPageSO[] journalPages;

        [Header("Events to switch input schemes when opening/closing journal:")]
        [SerializeField] 
        private GameEvent onJournalOpened;

        [SerializeField]
        private GameEvent onCloseButtonPressed;

        [Header("Events for analytics:")]
        [SerializeField]
        private GameEvent onOpenJournalUpdateAnalytics;

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
            onOpenJournalUpdateAnalytics.Raise();
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

        public void ShowKeyboardUI()
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(true);
        }

        public void ShowGamepadUI()
        {
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(false);
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