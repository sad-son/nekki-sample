using CameraAssembly;
using ServiceLocatorSystem;

namespace GameplayDependencies
{
    public sealed class GameplaySystemContainer : SystemLocatorBase<IGameplayDependency>
    {
        private readonly ICameraController _cameraController;
        
        public GameplaySystemContainer(ICameraController cameraController)
        {
            _cameraController = cameraController;
        }
        
        protected override void RegisterTypes()
        {
            Register(_cameraController);
        }
    }
}