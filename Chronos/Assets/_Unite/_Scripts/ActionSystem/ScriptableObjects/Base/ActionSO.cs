using UnityEngine;

namespace Unite.ActionSystem
{
    /// <summary>
    /// ActionSO - an abstract ScriptableObject that models an action that can be executed with/without a delay.
    /// Child classes contain the concrete implementations of different types of actions.
    ///
    /// The execution of actions are handled by the ActionExecutionManager class
    ///
    /// <seealso cref="PlayDialogueAction"/>
    /// <seealso cref="RaiseGameEventAction"/>
    /// <seealso cref="JournalPromptAction"/>
    /// </summary>
    public abstract class ActionSO : ScriptableObject
    {
        [SerializeField]
        private bool hasDelay;
        
        [SerializeField]
        private float delayInSeconds;

        public bool HasDelay => hasDelay;
        public float DelayInSeconds => delayInSeconds;
        
        public abstract void ExecuteAction();
    }
}