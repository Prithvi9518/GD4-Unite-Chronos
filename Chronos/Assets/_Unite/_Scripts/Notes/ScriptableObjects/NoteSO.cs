using UnityEngine;

namespace Unite.Notes
{
    [CreateAssetMenu(fileName = "NoteSO", menuName = "Notes/NoteSO")]
    public class NoteSO : ScriptableObject
    {
        [SerializeField]
        private string title;
        
        [SerializeField]
        [TextArea(2,20)]
        private string content;

        public string Title => title;
        public string Content => content;
    }
}