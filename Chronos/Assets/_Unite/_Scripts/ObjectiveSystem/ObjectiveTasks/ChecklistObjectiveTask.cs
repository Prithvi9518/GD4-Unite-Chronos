using System.Collections.Generic;
using UnityEngine;

namespace Unite.ObjectiveSystem
{
    public class ChecklistObjectiveTask : ObjectiveTask
    {
        [SerializeField] 
        private int countToReach;

        private int currentCount;
        private Dictionary<int, bool> checklistComplete = new();

        private void Awake()
        {
            for (int i = 1; i <= countToReach; i++)
            {
                checklistComplete.Add(i, false);
            }
        }

        public void UpdateChecklistAndCheckCompletion(int id)
        {
            checklistComplete.TryGetValue(id, out bool completed);
            if (completed) return;

            checklistComplete[id] = true;
            currentCount++;
            if(IsCountReached())
                CompleteTask();
        }

        private bool IsCountReached()
        {
            return currentCount >= countToReach;
        }
    }
}