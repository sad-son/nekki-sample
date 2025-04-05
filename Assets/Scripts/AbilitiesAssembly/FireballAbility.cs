using AbilitiesAssembly.Projectiles;
using CharacterAssembly;
using ServiceLocatorSystem;
using SpawnerAssembly;
using UnityEngine;

namespace AbilitiesAssembly
{
    public class FireballAbility : IAbility
    {
        private readonly AbilitiesParameters _abilitiesParameters;
        private readonly ProjectileSpawner _projectileSpawner;
        private readonly MyCharacterDependency _characterDependency;
        private readonly TargetSelector _targetSelector;

        public FireballAbility(AbilitiesParameters abilitiesParameters)
        {
            _abilitiesParameters = abilitiesParameters;
            _projectileSpawner = ServiceLocatorController.Resolve<SpawnerContainer>().ResolveDependency<ProjectileSpawner>();
            _targetSelector = ServiceLocatorController.Resolve<AbilitiesSystemContainer>().ResolveDependency<TargetSelector>();
            _characterDependency = ServiceLocatorController.Resolve<SpawnerContainer>().ResolveDependency<MyCharacterDependency>();
        }

        public void Execute()
        {
            if (!_targetSelector.HasTarget) return;
            
            var projectile = _projectileSpawner.Spawn(ProjectileType.Fireball);
            projectile.transform.SetPositionAndRotation(_characterDependency.Character.Aim.position, Quaternion.identity);
            projectile.SetTarget(new ProjectileParameters(
                lifeTime: _abilitiesParameters.LifeTime,
                maxDistance: _abilitiesParameters.MaxDistance,
                speed: _abilitiesParameters.Speed,
                damage: _abilitiesParameters.Damage,
                target: _targetSelector.TargetPosition));
        }
    }
}