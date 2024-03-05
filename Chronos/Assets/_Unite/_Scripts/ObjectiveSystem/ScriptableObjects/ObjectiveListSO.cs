using System.Collections.Generic;
using UnityEngine;

namespace Unite.ObjectiveSystem
{
    [CreateAssetMenu(fileName = "ObjectiveListSO", menuName = "Objective System/Objective List SO")]
    public class ObjectiveListSO : ScriptableObject
    {
        [SerializeField]
        private List<ObjectiveSO> objectives;

        public List<ObjectiveSO> Objectives => objectives;
    }
}