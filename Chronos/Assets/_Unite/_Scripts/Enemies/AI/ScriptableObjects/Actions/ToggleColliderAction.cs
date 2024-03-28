using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "ToggleColliderAction", menuName = "AI/Actions/Toggle Collider")]
    public class ToggleColliderAction : FSMAction
    {
        [SerializeField] 
        private bool colliderEnabled;
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            Collider collider = baseStateMachine.GetComponent<Collider>();
            collider.enabled = colliderEnabled;
        }
    }
}