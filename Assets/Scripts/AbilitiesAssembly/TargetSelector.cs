using Helpers;
using InputAssembly;
using ServiceLocatorAssembly;
using SpawnerAssembly;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AbilitiesAssembly
{
    public class TargetSelector : IAbilitySystemDependency
    {
        public Ray Ray { get; private set; }
        public bool HasTarget => _target != null;
        public Vector3 TargetPosition => !HasTarget ? Vector3.zero : _target.transform.position;
        
        private readonly InputExecutor _inputExecutor;
        
        private Enemy _target;
        
        public TargetSelector()
        {
            _inputExecutor = ServiceLocatorController.Resolve<InputSystemContainer>().ResolveDependency<InputExecutor>();
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
                    UnSelect();
                    _target = enemy;
                    _target.OnDeath += OnDeath;
                    _target.Select();
                }
            }
        }

        private void UnSelect()
        {
            if (!HasTarget) return;
            _target.OnDeath -= OnDeath;
            _target.Unselect();
        }

        private void OnDeath()
        {
            _target.OnDeath -= OnDeath;
            _target = null;
        }
    }
}