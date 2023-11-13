using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "ToggleColliderAction", menuName = "Unite/Scriptable Objects/AI/Actions/Toggle Collider")]
    public class ToggleColliderAction : Action
    {
        [SerializeField] 
        private bool colliderEnabled;
        public override void ExecuteAction(IStateMachine stateMachine)
        {
            EnemyStateMachine enemy = stateMachine as EnemyStateMachine;
            enemy.Collider.enabled = colliderEnabled;
        }
    }
}