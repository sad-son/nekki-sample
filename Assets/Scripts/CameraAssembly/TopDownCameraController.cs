using System;
using System.Collections.Generic;
using GameplayDependencies;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;

namespace CameraAssembly
{
    public class TopDownCameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TopDownCameraConfig _cameraConfig;
        
        private Dictionary<CameraState, CameraSetting> _cameraStates; 
        private CameraSetting _currentCameraSetting;
        private Transform _cameraTarget;

        private void OnValidate()
        {
            Assert.IsNotNull(_camera);
            Assert.IsNotNull(_cameraConfig);
        }

        private void Awake()
        {
            _cameraStates = _cameraConfig.GetSettingsDictionary();
            SetCameraState(CameraState.Idle);
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
            //  SetFieldOfView(GetCurrentCameraState.CameraFieldOfView);
        }

        private void SetCameraPosition(Vector3 targetPosition)
        {
            if (_currentCameraSetting.smoothMove)
            {
                _camera.transform.position = Vector3.Lerp(transform.position, targetPosition,
                    _currentCameraSetting.movementSpeed * Time.fixedDeltaTime);
            }
            else
            {
                _camera.transform.position = targetPosition;
            }
        }
    }
}