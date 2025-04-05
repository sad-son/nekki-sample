using System.Collections.Generic;
using InputAssembly;
using ServiceLocatorSystem;
using UnityEngine;

namespace AbilitiesAssembly
{
    public class AbilityExecutor : IAbilitySystemDependency
    {
        private readonly AbilityConfig _abilityConfig;
        private readonly Dictionary<AbilityType, AbilitiesParameters> _parametersDictionary;
        private Dictionary<AbilityType, IAbility> _abilitiesDictionary;

        private IAbility _currentAbility;
        private readonly InputExecutor _inputExecutor;
        
        public AbilityExecutor(AbilityConfig abilityConfig)
        {
            _abilityConfig = abilityConfig;
            _parametersDictionary = _abilityConfig.GetParametersDictionary();
            CreateAbilities();
            
            _inputExecutor = ServiceLocatorController.Resolve<InputSystemContainer>().ResolveDependency<InputExecutor>();
            _inputExecutor.OnAttack += OnAttack;
            EquipAbility(AbilityType.Fireball);
        }
        
        public void Dispose()
        {
            _inputExecutor.OnAttack -= OnAttack;
            _currentAbility = null;
            _abilitiesDictionary.Clear();
        }
        
        private void OnAttack()
        {
            Execute();
        }

        private void CreateAbilities()
        {
            _abilitiesDictionary = new()
            {
                [AbilityType.Fireball] = new FireballAbility(_parametersDictionary[AbilityType.Fireball])
            };
        }
        
        public void EquipAbility(AbilityType abilityType)
        {
            if (_abilitiesDictionary.TryGetValue(abilityType, out IAbility ability))
                _currentAbility = ability;
            else
            {
                Debug.LogError($"Ability {abilityType} not found");
            }
        }

        public void Execute()
        {
            _currentAbility?.Execute();
        }
    }
}