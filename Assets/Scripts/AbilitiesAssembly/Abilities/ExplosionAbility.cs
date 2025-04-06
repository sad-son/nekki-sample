using AbilitiesAssembly.Aoe;
using CharacterAssembly;
using ServiceLocatorAssembly;
using SpawnerAssembly;
using UnityEngine;

namespace AbilitiesAssembly.Abilities
{
    public class ExplosionAbility : IAbility
    {
        private readonly AoeAbilityParameters _aoeAbilityParameters;
        private readonly TargetSelector _targetSelector;
        private readonly MyCharacterDependency _characterDependency;
        private readonly AoeSpawner _aoeSpawner;

        public ExplosionAbility(AoeAbilityParameters aoeAbilityParameters)
        {
            _aoeAbilityParameters = aoeAbilityParameters;
            _aoeSpawner = ServiceLocatorController.Resolve<SpawnerContainer>().ResolveDependency<AoeSpawner>();
            _targetSelector = ServiceLocatorController.Resolve<AbilitiesSystemContainer>().ResolveDependency<TargetSelector>();
            _characterDependency = ServiceLocatorController.Resolve<CharacterContainer>().ResolveDependency<MyCharacterDependency>();
        }

        public void Execute()
        {
            if (!_targetSelector.HasTarget) return;
            _characterDependency.Character.transform.LookAt(_targetSelector.TargetPosition);
            var area = _aoeSpawner.Spawn(AoeType.Explosion);

            var aimPosition = _characterDependency.Character.Aim.position;
            var direction = (_targetSelector.TargetPosition - aimPosition).normalized;
            var distance = Vector3.Distance(_targetSelector.TargetPosition, aimPosition);
            var aoePoint = distance <= _aoeAbilityParameters.MaxDistance
                ? _targetSelector.TargetPosition
                : aimPosition + direction * _aoeAbilityParameters.MaxDistance;
            
            area.transform.SetPositionAndRotation(aoePoint, Quaternion.identity);
            
            area.Setup(new AoeParameters(
                _aoeAbilityParameters,
                target: _targetSelector.TargetPosition));
        }
    }
}