using Helpers;
using InputAssembly;
using ServiceLocatorSystem;
using SpawnerAssembly;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AbilitiesAssembly
{
    public class TargetSelector : IAbilitySystemDependency
    {
        public Ray Ray { get; private set; }
        
        private readonly InputExecutor _inputExecutor;
        
        private Enemy _target;
        public TargetSelector()
        {
            _inputExecutor = ServiceLocatorController.Resolve<InputSystemContainer>().Resolve<InputExecutor>();
            _inputExecutor.OnSelected += OnSelected;
        }

        public void Dispose()
        {
            _inputExecutor.OnSelected -= OnSelected;
        }

        private void OnSelected()
        {
            var mousePosition = Mouse.current.position.ReadValue();
            var mainCamera = Camera.main;
            
            if (mainCamera == null) return;
            
            Ray = mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(Ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag(TagsHelper.EnemyTag) && hit.collider.TryGetComponent<Enemy>(out var enemy))
                {
                    _target?.Unselect();
                    _target = enemy;
                    _target.Select();
                }
            }
        }
    }
}