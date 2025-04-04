using System.Collections.Generic;
using ServiceLocatorSystem;
using SpawnerAssembly;
using UnityEngine;
using UnityEngine.Assertions;

namespace CameraAssembly
{
    public class TopDownCameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TopDownCameraConfig _cameraConfig;
        
        private Dictionary<CameraState, CameraSetting> _cameraStates; 
        private CameraSetting _currentCameraSetting;
        private Transform _cameraTarget;
        private MyCharacterDependency _characterDependency;
        
        private void OnValidate()
        {
            Assert.IsNotNull(_camera);
            Assert.IsNotNull(_cameraConfig);
        }

        public void Initialize()
        {
            _cameraStates = _cameraConfig.GetSettingsDictionary();
            SetCameraState(CameraState.Idle);
            _characterDependency = ServiceLocatorController.Resolve<SpawnerContainer>().Resolve<MyCharacterDependency>();
            SetTarget(_characterDependency.Character);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            _cameraTarget = null;
            _cameraStates.Clear();
        }
        
        public void SetTarget(Transform target)
        {
            _cameraTarget = target;
        }
        
        public void SetCameraState(CameraState state)
        {
            _currentCameraSetting = _cameraStates[state];
        }

        private void LateUpdate()
        {
            if (!_cameraTarget) return;
            
            SetCameraPosition(_cameraTarget.position);
        }

        private void SetCameraPosition(Vector3 targetPosition)
        {
            if (_currentCameraSetting.smoothMove)
            {
                transform.transform.position = Vector3.Lerp(transform.position, targetPosition,
                    _currentCameraSetting.movementSpeed * Time.deltaTime);
            }
            else
            {
                transform.transform.position = targetPosition;
            }
        }
    }
}