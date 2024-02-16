using Unite.EventSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unite.Core.Input
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }

        [Header("Event for jump action")]
        [SerializeField]
        private GameEvent onPlayerJumpAction;
        
        [Header("Event for interact action")]
        [SerializeField] 
        private GameEvent onPlayerInteractAction;

        [Header("Event for use ability action")]
        [SerializeField] 
        private GameEvent onPlayerUseAbilityAction;

        [Header("Events for journal actions")]
        [SerializeField]
        private GameEvent onJournalOpenAction;
        
        [SerializeField]
        private GameEvent onJournalNextPageAction;
        
        [SerializeField]
        private GameEvent onJournalPreviousPageAction;

        [SerializeField]
        private GameEvent onJournalCloseAction;

        [SerializeField]
        private GamepadTypeEvent onGamepadUsed;
        [SerializeField]
        private GameEvent onKeyboardUsed;
        
        private PlayerInputActions playerInput;
        private PlayerInputActions.DefaultActions defaultActions;
        private PlayerInputActions.UIActions uiActions;
        private PlayerInputActions.JournalUIActions journalUIActions;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of InputManager in the scene! Destroying current instance.");
                Destroy(this);
            }

            Instance = this;
            
            playerInput = new PlayerInputActions();
            defaultActions = playerInput.Default;
            uiActions = playerInput.UI;
            journalUIActions = playerInput.JournalUI;
            
            SwitchToDefaultActionMap();
        }

        private void Start()
        {
            if (GamepadDetected(out Gamepad gamepad))
            {
                OnGamepadDetectedAtStart(gamepad);
            }
            else
            {
                onKeyboardUsed.Raise();
            }
        }

        private void OnEnable()
        {
            SubscribeToActions();
        }

        private void OnDisable()
        {
            UnsubscribeToActions();
        }

        private bool GamepadDetected(out Gamepad gamepad)
        {
            InputDevice[] devices = InputSystem.devices.ToArray();

            foreach (var device in devices)
            {
                if (device is not Gamepad g) continue;
                
                gamepad = g;
                return true;
            }

            gamepad = null;
            return false;
        }

        private void OnGamepadDetectedAtStart(Gamepad gamepad)
        {
            GamepadType type = GetGamepadType(gamepad);
            onGamepadUsed.Raise(type);
        }
        
        private void OnDeviceChanged(InputDevice device, InputDeviceChange change)
        {
            if (device is Gamepad gamepad && change == InputDeviceChange.Added)
            {
                GamepadType gamepadType = GetGamepadType(gamepad);
                onGamepadUsed.Raise(gamepadType);
            }
            else if (device is Gamepad && change == InputDeviceChange.Removed)
            {
                onKeyboardUsed.Raise();
            }
        }
        
        private GamepadType GetGamepadType(Gamepad gamepad)
        {
            if (gamepad.name.Contains("Xbox") || gamepad.name.Contains("XInputControllerWindows"))
            {
                return GamepadType.Xbox;
            }

            if (gamepad.name.Contains("DualShock") || gamepad.name.Contains("PS"))
            {
                return GamepadType.PlayStation;
            }

            return GamepadType.Unknown;
        }
        
        private void RaisePlayerJumpEvent(InputAction.CallbackContext ctx)
        {
            onPlayerJumpAction.Raise();
        }

        private void RaisePlayerUseAbilityEvent(InputAction.CallbackContext ctx)
        {
            onPlayerUseAbilityAction.Raise();
        }

        private void RaisePlayerInteractEvent(InputAction.CallbackContext ctx)
        {
            onPlayerInteractAction.Raise();
        }

        private void RaiseJournalOpenEvent(InputAction.CallbackContext ctx)
        {
            onJournalOpenAction.Raise();
        }
        
        private void RaiseJournalCloseEvent(InputAction.CallbackContext ctx)
        {
            onJournalCloseAction.Raise();
        }
        
        private void RaiseJournalNextPageEvent(InputAction.CallbackContext ctx)
        {
            onJournalNextPageAction.Raise();
        }
        
        private void RaiseJournalPreviousPageEvent(InputAction.CallbackContext ctx)
        {
            onJournalPreviousPageAction.Raise();
        }

        private void SubscribeToActions()
        {
            InputSystem.onDeviceChange += OnDeviceChanged;

            defaultActions.Jump.performed += RaisePlayerJumpEvent;
            defaultActions.Ability1.performed += RaisePlayerUseAbilityEvent;
            defaultActions.Interact.performed += RaisePlayerInteractEvent;
            defaultActions.JournalOpen.performed += RaiseJournalOpenEvent;

            journalUIActions.CloseJournal.performed += RaiseJournalCloseEvent;
            journalUIActions.NextPage.performed += RaiseJournalNextPageEvent;
            journalUIActions.PreviousPage.performed += RaiseJournalPreviousPageEvent;
        }

        private void UnsubscribeToActions()
        {
            InputSystem.onDeviceChange -= OnDeviceChanged;

            defaultActions.Jump.performed -= RaisePlayerJumpEvent;
            defaultActions.Ability1.performed -= RaisePlayerUseAbilityEvent;
            defaultActions.Interact.performed -= RaisePlayerInteractEvent;
            defaultActions.JournalOpen.performed -= RaiseJournalOpenEvent;

            journalUIActions.CloseJournal.performed -= RaiseJournalCloseEvent;
            journalUIActions.NextPage.performed -= RaiseJournalNextPageEvent;
            journalUIActions.PreviousPage.performed -= RaiseJournalPreviousPageEvent;
        }
        
        public bool IsShootActionPressed() => defaultActions.Shoot.IsPressed();

        public Vector2 GetMovementVectorNormalized()
        {
            Vector2 inputVector = defaultActions.Move.ReadValue<Vector2>();
            return inputVector.normalized;
        }

        public Vector2 GetLookVectorNormalized()
        {
            Vector2 lookVector = defaultActions.Look.ReadValue<Vector2>();
            return lookVector.normalized;
        }

        public void SwitchToDefaultActionMap()
        {
            defaultActions.Enable();
            uiActions.Disable();
            journalUIActions.Disable();
        }
        
        public void SwitchToUIActionMap()
        {
            defaultActions.Disable();
            uiActions.Enable();
            journalUIActions.Disable();
        }

        public void SwitchToJournalUIActionMap()
        {
            defaultActions.Disable();
            uiActions.Disable();
            journalUIActions.Enable();
        }
    }
}