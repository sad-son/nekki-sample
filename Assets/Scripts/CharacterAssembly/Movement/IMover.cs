using UnityEngine;

namespace CharacterAssembly.Movement
{
    public interface IMover
    {
        void Move(float deltaTime);
        Vector3 GetVelocity(float deltaTime);
    }
}