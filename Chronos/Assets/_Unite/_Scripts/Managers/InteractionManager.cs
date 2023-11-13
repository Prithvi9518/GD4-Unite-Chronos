using System;
using UnityEngine;

namespace Unite
{
    public class InteractionManager : MonoBehaviour
    {
        private IRayProvider rayProvider;
        private ISelector selector;

        [SerializeField]
        private SelectionResponse[] selectionResponses;

        [SerializeField]
        private PlayerInputHandler inputHandler;

        private Transform currentSelection;

        private void Awake()
        {
            rayProvider = GetComponent<IRayProvider>();
            selector = GetComponent<ISelector>();
        }

        private void Update()
        {
            if (inputHandler == null) return;
            
            ResetSelection();

            selector.CheckSelection(rayProvider.ProvideRay());
            currentSelection = selector.GetSelection();

            ExecuteSelectionResponses();

            DoInteraction();
        }
        
        private void DoInteraction()
        {
            if (currentSelection == null) return; 

            IInteractible interactible = currentSelection.GetComponent<IInteractible>();
            if (interactible == null) return;

            if (inputHandler.DefaultActions.Interact.triggered)
            {
                interactible.Interact();
            }
        }

        private void ExecuteSelectionResponses()
        {
            if (currentSelection != null)
            {
                foreach (SelectionResponse response in selectionResponses)
                {
                    response.OnSelect(currentSelection);
                }
            }
        }

        private void ResetSelection()
        {
            if (currentSelection != null)
            {
                foreach (SelectionResponse response in selectionResponses)
                {
                    response.OnDeselect(currentSelection);
                }
            }
        }
    }
}


