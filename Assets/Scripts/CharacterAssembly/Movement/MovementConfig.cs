using System.Collections.Generic;
using UnityEngine;

namespace CharacterAssembly.Movement
{
    [CreateAssetMenu(menuName = "Data/" + nameof(MovementConfig), fileName = nameof(MovementConfig))]
    public class MovementConfig : ScriptableObject
    {
        [SerializeField] private List<MovementSettings> _movementSettings;

        public Dictionary<MoverType, MovementSettings> GetSettingsDictionary()
        {
            var result = new Dictionary<MoverType, MovementSettings>();
            foreach (var movementSettings in _movementSettings)
            {
                if (!result.TryAdd(movementSettings.MoverType, movementSettings))
                {
                    Debug.LogWarning($"Duplicate setting: {movementSettings.MoverType}");
                }
            }
            
            return result;
        }
    }
}