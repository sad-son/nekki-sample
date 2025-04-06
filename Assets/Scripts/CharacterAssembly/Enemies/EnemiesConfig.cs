using System.Collections.Generic;
using UnityEngine;

namespace SpawnerAssembly
{
    [CreateAssetMenu(menuName = "Data/" + nameof(EnemiesConfig), fileName = nameof(EnemiesConfig))]
    public class EnemiesConfig : ScriptableObject
    {
        [SerializeField] private List<EnemiesParameters> _enemiesParameters;

        public Dictionary<EnemiesType, EnemiesParameters> GetParametersDictionary()
        {
            var result = new Dictionary<EnemiesType, EnemiesParameters>();
            foreach (var parameters in _enemiesParameters)
            {
                if (!result.TryAdd(parameters.EnemiesType, parameters))
                {
                    Debug.LogWarning($"Duplicate setting: {parameters.EnemiesType}");
                }
            }
            
            return result;
        }
    }
}