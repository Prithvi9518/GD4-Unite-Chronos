using Unite.EventSystem;
using Unite.JournalSystem;
using UnityEngine;

namespace Unite.ActionSystem
{
    /// <summary>
    /// ActionSO used to raise a journal prompt event
    /// </summary>
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