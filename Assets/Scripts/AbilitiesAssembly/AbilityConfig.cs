using System.Collections.Generic;
using UnityEngine;

namespace AbilitiesAssembly
{
    [CreateAssetMenu(menuName = "Data/" + nameof(AbilityConfig), fileName = nameof(AbilityConfig))]
    public class AbilityConfig : ScriptableObject
    {
        [SerializeField] private List<AbilityParameters> _abilityParameters;

        private Dictionary<AbilityType, AbilityParameters> _cachedAbilityParametersDictionary;
    
        public T GetAbilityParameters<T>(AbilityType abilityType) where T : AbilityParameters
        {
            _cachedAbilityParametersDictionary ??= GetParametersDictionary();
            
            if (_cachedAbilityParametersDictionary.TryGetValue(abilityType, out var parameters) 
                && parameters is T value) 
                return value;

            return null;
        }

        private Dictionary<AbilityType, AbilityParameters> GetParametersDictionary()
        {
            var result = new Dictionary<AbilityType, AbilityParameters>();
            foreach (var parameters in _abilityParameters)
            {
                if (!result.TryAdd(parameters.AbilityType, parameters))
                {
                    Debug.LogWarning($"Duplicate setting: {parameters.AbilityType}");
                }
            }
            
            return result;
        }
    }
}