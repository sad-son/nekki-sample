using System.Collections.Generic;
using UnityEngine;

namespace AbilitiesAssembly
{
    [CreateAssetMenu(menuName = "Data/" + nameof(AbilityConfig), fileName = nameof(AbilityConfig))]
    public class AbilityConfig : ScriptableObject
    {
        [SerializeField] private List<ProjectileAbilityParameters> _projectileParameters;
        [SerializeField] private List<AoeAbilityParameters> _aoeParameters;

        public Dictionary<AbilityType, ProjectileAbilityParameters> GetProjectileParametersDictionary()
        {
            var result = new Dictionary<AbilityType, ProjectileAbilityParameters>();
            foreach (var parameters in _projectileParameters)
            {
                if (!result.TryAdd(parameters.AbilityType, parameters))
                {
                    Debug.LogWarning($"Duplicate setting: {parameters.AbilityType}");
                }
            }
            
            return result;
        }
        
        public Dictionary<AbilityType, AoeAbilityParameters> GetAoeParametersDictionary()
        {
            var result = new Dictionary<AbilityType, AoeAbilityParameters>();
            foreach (var parameters in _aoeParameters)
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