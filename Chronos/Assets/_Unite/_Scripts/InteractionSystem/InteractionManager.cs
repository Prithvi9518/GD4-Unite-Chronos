using Unite.EventSystem;
using UnityEngine;

namespace Unite.InteractionSystem
{
    public class InteractionManager : MonoBehaviour
    {
        private IRayProvider rayProvider;
        private ISelector selector;

        [SerializeField]
        private SelectionResponse[] selectionResponses;

        [SerializeField]
        private GameEvent onSelectionNullAfterReset;

        private Transform currentSelection;

        private void Awake()
        {
            rayProvider = GetComponent<IRayProvider>();
            selector = GetComponent<ISelector>();
        }

        private void Update()
        {
            ResetSelection();
            UpdateSelection();
            ExecuteSelectionResponses();
        }
        
        public void DoInteraction()
        {
            if (currentSelection == null) return; 

            if (!currentSelection.TryGetComponent(out InteractibleObject interactible)) return;
            interactible.HandleInteraction();
        }

        private void UpdateSelection()
        {
            selector.CheckSelection(rayProvider.ProvideRay());
            currentSelection = selector.GetSelection();
        }

        private void ExecuteSelectionResponses()
        {
            if (currentSelection == null) return;
            foreach (SelectionResponse response in selectionResponses)
            {
                response.OnSelect(currentSelection);
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
            else
            {
                onSelectionNullAfterReset.Raise();
            }
        }
    }
}


