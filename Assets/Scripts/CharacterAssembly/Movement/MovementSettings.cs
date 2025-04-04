using System;

namespace CharacterAssembly.Movement
{
    [Serializable]
    public class MovementSettings
    {
        public MoverType moverType;
        public float movementSpeed;
        public float rotationSpeed;
    }
}