using UnityEngine;


namespace Unite.Core.InteractionSystem
{
    public class RaycastSelector : MonoBehaviour, ISelector
    {
        [SerializeField]
        private float maxDistance;

        [SerializeField]
        private string selectableTag;

        [SerializeField]
        private LayerMask layerMask;

        private Transform selection;
        private RaycastHit hitInfo;

        public void CheckSelection(Ray ray)
        {
            selection = null;

            if(Physics.Raycast(ray, out hitInfo, maxDistance, layerMask))
            {
                Transform currentSelection = hitInfo.transform;
                if(currentSelection.CompareTag(selectableTag))
                {
                    selection = currentSelection;
                }
            }
        }

        public RaycastHit GetHitInfo()
        {
            return hitInfo;
        }

        public Transform GetSelection()
        {
            return selection;
        }
    }
}

