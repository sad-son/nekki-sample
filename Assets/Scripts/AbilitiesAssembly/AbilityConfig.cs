using System.Collections.Generic;
using UnityEngine;

namespace AbilitiesAssembly
{
    [CreateAssetMenu(menuName = "Data/" + nameof(AbilityConfig), fileName = nameof(AbilityConfig))]
    public class AbilityConfig : ScriptableObject
    {
        [SerializeField] private List<AbilitiesParameters> _abilitiesParameters;

        public Dictionary<AbilityType, AbilitiesParameters> GetParametersDictionary()
        {
            var result = new Dictionary<AbilityType, AbilitiesParameters>();
            foreach (var parameters in _abilitiesParameters)
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