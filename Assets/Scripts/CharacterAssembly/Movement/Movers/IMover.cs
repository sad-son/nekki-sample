using UnityEngine;

namespace CharacterAssembly.Movement.Movers
{
    public interface IMover
    {
        void Enter();
        void Exit();
        void Move(float deltaTime, Vector3 movementDirection);
        Vector3 GetVelocity(float deltaTime);
        Quaternion GetRotation(Transform transform, float deltaTime);
    }
}