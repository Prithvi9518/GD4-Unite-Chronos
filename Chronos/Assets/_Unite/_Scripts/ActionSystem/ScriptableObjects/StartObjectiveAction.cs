using Unite.EventSystem;
using Unite.ObjectiveSystem;
using UnityEngine;

namespace Unite.ActionSystem
{
    [CreateAssetMenu(fileName = "StartObjectiveAction", menuName = "Action System/Start Objective Action")]
    public class StartObjectiveAction : ActionSO
    {
        [SerializeField]
        private ObjectiveSO objectiveToStart;
        
        [SerializeField]
        private StringEvent startObjectiveEvent;
        
        public override void ExecuteAction()
        {
            startObjectiveEvent.Raise(objectiveToStart.name);
        }
    }
}