using Unite.EventSystem;
using Unite.ObjectiveSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class ActiveObjectiveCheckInteractible : InteractibleObject
    {
        [SerializeField]
        private ObjectiveSO objectiveToCheck;
        
        [SerializeField]
        private GameEvent onObjectiveNotActive;
        
        [SerializeField]
        private GameEvent onObjectiveActive;

        public override void HandleInteraction()
        {
            base.HandleInteraction();

            if (IsObjectiveActive())
                onObjectiveActive.Raise();
            else
                onObjectiveNotActive.Raise();
        }

        private bool IsObjectiveActive()
        {
            if (ObjectiveManager.Instance == null) return false;
            return ObjectiveManager.Instance.IsObjectiveActive(objectiveToCheck);
        }
    }
}