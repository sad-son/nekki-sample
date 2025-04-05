using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputAssembly
{
    public class InputExecutor : IInputSystemDependency
    {
        private readonly PlayerInputActions _inputActions;

        public event Action OnSelected;
        
        public Vector2 MoveInput { get; private set; }
        public bool Sprint { get; private set; }
        
        public InputExecutor()
        {
            _inputActions = new PlayerInputActions();
            _inputActions.Enable();
            _inputActions.Player.Move.performed += OnMove;
            _inputActions.Player.Move.canceled += OnMoveCanceled;
            _inputActions.Player.Sprint.performed += OnSprint;
            _inputActions.Player.Sprint.canceled += OnSprintCanceled;
            _inputActions.Player.Select.performed += OnSelectPerformed;
        }
        
        public void Dispose()
        {
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Move.canceled -= OnMoveCanceled;
            _inputActions.Player.Sprint.performed -= OnSprint;
            _inputActions.Player.Sprint.canceled -= OnSprintCanceled;
            _inputActions.Player.Select.performed -= OnSelectPerformed;
            _inputActions.Disable();
            _inputActions.Dispose();
        }

        private void OnSelectPerformed(InputAction.CallbackContext obj)
        {
            OnSelected?.Invoke();
        }

        private void OnSprint(InputAction.CallbackContext obj)
        {
            Sprint = true;
        }
        
        private void OnSprintCanceled(InputAction.CallbackContext obj)
        {
            Sprint = false;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
        
        private void OnMoveCanceled(InputAction.CallbackContext obj)
        {
            MoveInput = Vector2.zero;
        }
    }
}