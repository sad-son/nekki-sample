﻿using System;
using System.Collections.Generic;
using System.Linq;
using AbilitiesAssembly.Abilities;
using AbilitiesAssembly.Parameters;
using InputAssembly;
using ServiceLocatorAssembly;
using UnityEngine;

namespace AbilitiesAssembly
{
    public class AbilityExecutor : IAbilitySystemDependency
    {
        public AbilityType AbilityType { get; private set; }
        public event Action<AbilityType> OnAbilitySelected;
        
        private readonly AbilityConfig _abilityConfig;
        private readonly InputExecutor _inputExecutor;
        
        private Dictionary<AbilityType, IAbility> _abilitiesDictionary;
        private AbilityType[] _abilityTypes;
        private IAbility _currentAbility;
        private int _selectedAbilityIndex;
        
        public AbilityExecutor(AbilityConfig abilityConfig)
        {
            _abilityConfig = abilityConfig;
            CreateAbilities();
            
            _inputExecutor = ServiceLocatorController.Resolve<InputSystemContainer>().ResolveDependency<InputExecutor>();
            _inputExecutor.OnAttack += OnAttack;
            _inputExecutor.OnNextAbility += OnNextAbility;
            _inputExecutor.OnPreviousAbility += OnPreviousAbility;
            EquipAbility(AbilityType.Fireball);
        }
        
        public void Dispose()
        {
            _inputExecutor.OnAttack -= OnAttack;
            _inputExecutor.OnNextAbility -= OnNextAbility;
            _inputExecutor.OnPreviousAbility -= OnPreviousAbility;
            _currentAbility = null;
            _abilitiesDictionary.Clear();
        }

        private void OnPreviousAbility()
        {
            _selectedAbilityIndex = (_selectedAbilityIndex - 1 + _abilityTypes.Length) % _abilityTypes.Length;
            EquipAbility(_abilityTypes[_selectedAbilityIndex]);
        }

        private void OnNextAbility()
        {
            _selectedAbilityIndex = (_selectedAbilityIndex + 1) % _abilityTypes.Length;
            EquipAbility(_abilityTypes[_selectedAbilityIndex]);
        }

        private void OnAttack()
        {
            Execute();
        }

        private void CreateAbilities()
        {
            var fireballParams = _abilityConfig.GetAbilityParameters<ProjectileAbilityParameters>(AbilityType.Fireball);
            var explosionParams = _abilityConfig.GetAbilityParameters<AoeAbilityParameters>(AbilityType.Explosion);
            
            _abilitiesDictionary = new()
            {
                [AbilityType.Fireball] = new FireballAbility(fireballParams),
                [AbilityType.Explosion] = new ExplosionAbility(explosionParams)
            };

            _abilityTypes = _abilitiesDictionary.Keys.ToArray();
        }
        
        public void EquipAbility(AbilityType abilityType)
        {
            if (_abilitiesDictionary.TryGetValue(abilityType, out IAbility ability))
            {
                _currentAbility = ability;
                AbilityType = abilityType;
                OnAbilitySelected?.Invoke(abilityType);
            }
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