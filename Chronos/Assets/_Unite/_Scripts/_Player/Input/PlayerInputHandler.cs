using Unite.EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unite.Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInputActions playerInput;
        private PlayerInputActions.DefaultActions defaultActions;

        public PlayerInputActions.DefaultActions DefaultActions => defaultActions;

        [Header("Event for interact action")]
        [SerializeField] 
        private GameEvent onPlayerInteractAction;

        [Header("Event for use ability action")]
        [SerializeField] 
        private GameEvent onPlayerUseAbilityAction;
        
        private void Awake()
        {
            playerInput = new PlayerInputActions();
            defaultActions = playerInput.Default;
        }

        private void OnEnable()
        {
            defaultActions.Enable();
            SubscribeToActions();
        }

        private void OnDisable()
        {
            defaultActions.Disable();
            UnsubscribeToActions();
        }

        public bool IsShootActionPressed() => defaultActions.Shoot.IsPressed();

        private void RaisePlayerUseAbilityEvent(InputAction.CallbackContext ctx)
        {
            onPlayerUseAbilityAction.Raise();
        }

        private void RaisePlayerInteractEvent(InputAction.CallbackContext ctx)
        {
            onPlayerInteractAction.Raise();
        }

        private void SubscribeToActions()
        {
            defaultActions.Ability1.performed += RaisePlayerUseAbilityEvent;
            defaultActions.Interact.performed += RaisePlayerInteractEvent;
        }

        private void UnsubscribeToActions()
        {
            defaultActions.Ability1.performed -= RaisePlayerUseAbilityEvent;
            defaultActions.Interact.performed -= RaisePlayerInteractEvent;
        }
        
    }
}