using System.Collections.Generic;
using UnityEngine;

namespace AbilitiesAssembly.Aoe
{
    [CreateAssetMenu(menuName = "Data/" + nameof(AoeConfig), fileName = nameof(AoeConfig))]
    public class AoeConfig : ScriptableObject
    {
        [SerializeField] private List<AoeArea> _aoeAreas;

        public Dictionary<AoeType, AoeArea> GetDictionary()
        {
            var result = new Dictionary<AoeType, AoeArea>();
            foreach (var area in _aoeAreas)
            {
                if (!result.TryAdd(area.Type, area))
                {
                    Debug.LogWarning($"Duplicate setting: {area.GetType()}");
                }
            }
            
            return result;
        }
    }
}