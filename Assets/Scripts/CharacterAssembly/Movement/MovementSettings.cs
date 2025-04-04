using System;

namespace CharacterAssembly.Movement
{
    [Serializable]
    public struct MovementSettings
    {
        public MoverType moverType;
        public float movementSpeed;
    }
}