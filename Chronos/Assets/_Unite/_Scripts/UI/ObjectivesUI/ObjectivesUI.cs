using System;
using System.Collections.Generic;
using Unite.ObjectiveSystem;
using UnityEngine;

namespace Unite.UI
{
    public class ObjectivesUI : MonoBehaviour
    {
        [SerializeField]
        private Transform container;
        [SerializeField]
        private Transform objectiveTemplate;


        private void Awake()
        {
            objectiveTemplate.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            UpdateUI();
        }

        private void OnDisable()
        {
            UpdateUI();
        }

        public void HandleObjectiveStarted()
        {
            UpdateUI();
        }

        public void HandleObjectiveFinished()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            foreach (Transform child in container)
            {
                if(child == objectiveTemplate) continue;
                Destroy(child.gameObject);
            }

            List<Objective> activeObjectives = ObjectiveManager.Instance.ActiveObjectives;
            foreach (Objective objective in activeObjectives)
            {
                if (!objective.CurrentTaskExists()) continue;
                
                Transform taskTransform = Instantiate(objectiveTemplate, container);
                taskTransform.gameObject.SetActive(true);
                taskTransform.GetComponent<ObjectiveTemplateUI>().SetObjectiveText(objective);
            }
        }
    }
}