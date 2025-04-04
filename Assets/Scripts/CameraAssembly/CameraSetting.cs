using System;

namespace CameraAssembly
{
    [Serializable]
    public class CameraSetting
    {
        public CameraState state;
        public float movementSpeed;
        public bool smoothMove;
    }
}