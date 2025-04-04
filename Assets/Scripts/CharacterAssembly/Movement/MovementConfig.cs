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
                if (!result.TryAdd(movementSettings.moverType, movementSettings))
                {
                    Debug.LogWarning($"Duplicate setting: {movementSettings.moverType}");
                }
            }
            
            return result;
        }
    }
}