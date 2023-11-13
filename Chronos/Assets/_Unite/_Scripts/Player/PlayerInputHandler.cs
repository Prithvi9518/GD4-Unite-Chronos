using UnityEngine;

namespace Unite
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerInputActions playerInput;
        private PlayerInputActions.DefaultActions defaultActions;

        public PlayerInputActions.DefaultActions DefaultActions => defaultActions;

        private void Awake()
        {
            playerInput = new PlayerInputActions();
            defaultActions = playerInput.Default;
        }

        private void OnEnable()
        {
            defaultActions.Enable();
        }

        private void OnDisable()
        {
            defaultActions.Disable();
        }
    }
}