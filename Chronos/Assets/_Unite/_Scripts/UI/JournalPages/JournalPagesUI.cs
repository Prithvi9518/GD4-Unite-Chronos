using UnityEngine;

namespace Unite.UI
{
    public class JournalPagesUI : MonoBehaviour
    {
        [SerializeField]
        private Transform mainPanel;
        
        private void Awake()
        {
            mainPanel.gameObject.SetActive(false);
        }
    }
}