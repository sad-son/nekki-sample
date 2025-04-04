using UnityEngine;
using UnityEngine.InputSystem;

namespace InputAssembly
{
    public class InputExecutor : IInputSystemDependency
    {
        private readonly PlayerInputActions _inputActions;
        
        public Vector2 MoveInput { get; private set; } 
        
        public InputExecutor()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Enable();
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMoveCanceled;
        }
        
        public void Dispose()
        {
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Move.canceled -= OnMoveCanceled;
            _inputActions.Disable();
            _inputActions.Dispose();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
            Debug.Log($"Move Input {MoveInput.x}, {MoveInput.y}");
        }

        private void OnMoveCanceled(InputAction.CallbackContext obj)
        {
            MoveInput = Vector2.zero;
            Debug.Log($"Move Input {MoveInput.x}, {MoveInput.y}");
        }
    }
}