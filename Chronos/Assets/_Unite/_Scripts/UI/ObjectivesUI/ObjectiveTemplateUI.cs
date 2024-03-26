using TMPro;
using Unite.ObjectiveSystem;
using UnityEngine;

namespace Unite.UI
{
    public class ObjectiveTemplateUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI objectiveText;

        public void SetObjectiveText(Objective objective)
        {
            objectiveText.text = objective.ObjectiveData.ObjectiveDescription;
        }
    }
}