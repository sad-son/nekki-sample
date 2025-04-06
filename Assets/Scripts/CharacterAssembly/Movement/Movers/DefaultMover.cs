using CharacterAssembly.Animation;
using UnityEngine;

namespace CharacterAssembly.Movement.Movers
{
    public abstract class DefaultMover : IMover
    {
        private readonly CharacterController _characterController;
        private readonly MovementSettings _movementSettings;
        
        protected readonly AnimatorController AnimatorController;
        protected readonly MovementController MovementController;
        
        private Vector3 _movementDirection;

        protected DefaultMover(MovementController movementController, MovementSettings settings)
        {
            MovementController = movementController;
            AnimatorController = movementController.AnimatorController;
            
            _characterController = movementController.CharacterController;
            _movementSettings = settings;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }
        
        public void Move(float deltaTime, Vector3 movementDirection)
        {
            _movementDirection = movementDirection;
            _characterController.Move(GetVelocity(deltaTime));
            _characterController.transform.rotation = GetRotation(_characterController.transform, deltaTime);
        }

        public Quaternion GetRotation(Transform transform, float deltaTime)
        {
            var targetRotation = Quaternion.LookRotation(_movementDirection);
            return Quaternion.Slerp(transform.rotation, targetRotation, _movementSettings.RotationSpeed * deltaTime);
        }

        public Vector3 GetVelocity(float deltaTime)
        {
            return _movementDirection * (_movementSettings.MovementSpeed * deltaTime);
        }
    }
}