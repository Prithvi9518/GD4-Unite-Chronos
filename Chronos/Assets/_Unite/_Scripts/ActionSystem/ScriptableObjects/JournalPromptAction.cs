using Unite.EventSystem;
using Unite.JournalSystem;
using UnityEngine;

namespace Unite.ActionSystem
{
    [CreateAssetMenu(fileName = "JournalPromptAction", menuName = "Action System/JournalPromptAction")]
    public class JournalPromptAction : ActionSO
    {
        [SerializeField] private JournalPageSO promptedPage;
        [SerializeField] private JournalPageSOEvent promptEvent;
        public override void ExecuteAction()
        {
            promptEvent.Raise(promptedPage);
        }
    }
}