using UnityEngine;

namespace Unite.JournalSystem
{
    [CreateAssetMenu(fileName = "JournalPageSO", menuName = "Journal Pages/Journal Page SO")]
    public class JournalPageSO : ScriptableObject
    {
        [SerializeField]
        [TextArea(2,10)]
        private string title;

        [SerializeField]
        private bool hasTwoPages;

        [SerializeField]
        private Sprite[] pageSprites;

        public string Title => title;
        public bool HasTwoPages => hasTwoPages;
        public Sprite[] PageSprites => pageSprites;
    }
}