using UnityEngine;

namespace Unite.Core.InteractionSystem
{
    public class InteractionManager : MonoBehaviour
    {
        private IRayProvider rayProvider;
        private ISelector selector;

        [SerializeField]
        private SelectionResponse[] selectionResponses;

        private Transform currentSelection;

        private void Awake()
        {
            rayProvider = GetComponent<IRayProvider>();
            selector = GetComponent<ISelector>();
        }

        private void Update()
        {
            ResetSelection();

            selector.CheckSelection(rayProvider.ProvideRay());
            currentSelection = selector.GetSelection();

            ExecuteSelectionResponses();
        }
        
        public void DoInteraction()
        {
            if (currentSelection == null) return; 

            IInteractible interactible = currentSelection.GetComponent<IInteractible>();
            interactible?.Interact();
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


