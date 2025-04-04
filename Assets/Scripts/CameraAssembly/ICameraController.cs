using GameplayDependencies;
using UnityEngine;

namespace CameraAssembly
{
    public interface ICameraController : IGameplayDependency
    {
        void SetTarget(Transform target);
        void SetCameraState(CameraState state);
    }
}