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

        [Header("Event for shooting action")]
        [SerializeField]
        private GameEvent onPlayerShootAction;

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

        private void RaisePlayerShootEvent(InputAction.CallbackContext ctx)
        {
            onPlayerShootAction.Raise();
        }

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
            defaultActions.Shoot.performed += RaisePlayerShootEvent;
            defaultActions.Ability1.performed += RaisePlayerUseAbilityEvent;
            defaultActions.Interact.performed += RaisePlayerInteractEvent;
        }

        private void UnsubscribeToActions()
        {
            defaultActions.Shoot.performed -= RaisePlayerShootEvent;
            defaultActions.Ability1.performed -= RaisePlayerUseAbilityEvent;
            defaultActions.Interact.performed -= RaisePlayerInteractEvent;
        }
        
    }
}