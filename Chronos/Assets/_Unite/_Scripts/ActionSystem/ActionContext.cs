using UnityEngine;

namespace Unite.ActionSystem
{
    /// <summary>
    /// Wrapper class for an ActionSO, to control whether the action can only be performed once.
    /// </summary>
    [System.Serializable]
    public class ActionContext
    {
        [SerializeField] private ActionSO action;
        [SerializeField] private bool doOnce;

        private bool executedOnce;

        public ActionSO Action => action;
        public bool DoOnce => doOnce;
        public bool ExecutedOnce => executedOnce;

        public void RegisterFirstExecution()
        {
            executedOnce = true;
        }
    }
}