using System;

namespace CameraAssembly
{
    [Serializable]
    public struct CameraSetting
    {
        public CameraState state;
        public float movementSpeed;
        public bool smoothMove;
    }
}