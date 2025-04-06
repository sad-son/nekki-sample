using AbilitiesAssembly.Parameters;
using AbilitiesAssembly.Projectiles;
using CharacterAssembly;
using ServiceLocatorAssembly;
using SpawnerAssembly;
using UnityEngine;

namespace AbilitiesAssembly.Abilities
{
    public class FireballAbility : IAbility
    {
        private readonly ProjectileAbilityParameters _projectileAbilityParameters;
        private readonly ProjectileSpawner _projectileSpawner;
        private readonly MyCharacterDependency _characterDependency;
        private readonly TargetSelector _targetSelector;

        public FireballAbility(ProjectileAbilityParameters projectileAbilityParameters)
        {
            _projectileAbilityParameters = projectileAbilityParameters;
            _projectileSpawner = ServiceLocatorController.Resolve<SpawnerContainer>().ResolveDependency<ProjectileSpawner>();
            _targetSelector = ServiceLocatorController.Resolve<AbilitiesSystemContainer>().ResolveDependency<TargetSelector>();
            _characterDependency = ServiceLocatorController.Resolve<CharacterContainer>().ResolveDependency<MyCharacterDependency>();
        }

        public void Execute()
        {
            if (!_targetSelector.HasTarget) return;
            _characterDependency.Character.transform.LookAt(_targetSelector.TargetPosition);
            var projectile = _projectileSpawner.Spawn(ProjectileType.Fireball);
            projectile.transform.SetPositionAndRotation(_characterDependency.Character.Aim.position, Quaternion.identity);
            
            projectile.SetTarget(new ProjectileParameters(
                lifeTime: _projectileAbilityParameters.LifeTime,
                maxDistance: _projectileAbilityParameters.MaxDistance,
                speed: _projectileAbilityParameters.Speed,
                damage: _projectileAbilityParameters.Damage,
                target: _targetSelector.TargetPosition));
        }
    }
}