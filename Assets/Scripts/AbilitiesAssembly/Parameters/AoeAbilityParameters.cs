using UnityEngine;

namespace AbilitiesAssembly.Parameters
{
    [CreateAssetMenu(menuName = "Data/Abilities/" + nameof(AoeAbilityParameters), fileName = nameof(AoeAbilityParameters))]
    public class AoeAbilityParameters  : AbilityParameters
    {
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
    }
}