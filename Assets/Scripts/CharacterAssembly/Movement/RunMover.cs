using InputAssembly;
using ServiceLocatorSystem;
using UnityEngine;

namespace CharacterAssembly.Movement
{
    public class RunMover : IMover
    {
        private readonly CharacterController _characterController;
        private readonly InputExecutor _inputExecutor;
        private readonly MovementSettings _movementSettings;
        
        public RunMover(CharacterController characterController, MovementSettings settings)
        {
            _characterController = characterController;
            _movementSettings = settings;
            _inputExecutor = ServiceLocatorController.Resolve<InputSystemContainer>().Resolve<InputExecutor>();
        }

        public void Move(float deltaTime)
        {
            _characterController.Move(GetVelocity(deltaTime));
        }

        public Vector3 GetVelocity(float deltaTime)
        {
            var moveInput = _inputExecutor.MoveInput;
            var moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            return moveDirection * _movementSettings.movementSpeed * deltaTime;
        }
    }
}