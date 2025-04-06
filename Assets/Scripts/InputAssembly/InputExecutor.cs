using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputAssembly
{
    public class InputExecutor : IInputSystemDependency
    {
        private readonly PlayerInputActions _inputActions;

        public event Action OnNextAbility;
        public event Action OnPreviousAbility;
        public event Action OnSelected;
        public event Action OnAttack;
        
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
            _inputActions.Player.Attack.performed += OnAttackPerformed;
            _inputActions.Player.NextAbility.performed += OnNextAbilityPerformed;
            _inputActions.Player.PreviousAbility.performed += OnPreviousAbilityPerformed;
        }
        
        public void Dispose()
        {
            _inputActions.Player.Move.performed -= OnMove;
            _inputActions.Player.Move.canceled -= OnMoveCanceled;
            _inputActions.Player.Sprint.performed -= OnSprint;
            _inputActions.Player.Sprint.canceled -= OnSprintCanceled;
            _inputActions.Player.Select.performed -= OnSelectPerformed;
            _inputActions.Player.Attack.performed -= OnAttackPerformed;
            _inputActions.Player.NextAbility.performed -= OnNextAbilityPerformed;
            _inputActions.Player.PreviousAbility.performed -= OnPreviousAbilityPerformed;
            _inputActions.Disable();
            _inputActions.Dispose();
        }

        private void OnNextAbilityPerformed(InputAction.CallbackContext obj)
        {
            OnNextAbility?.Invoke();
        }

        private void OnPreviousAbilityPerformed(InputAction.CallbackContext obj)
        {
            OnPreviousAbility?.Invoke();
        }

        private void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            OnAttack?.Invoke();
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