using UnityEngine;

namespace AbilitiesAssembly
{
    public class AbilityParameters : ScriptableObject
    {
        [field: SerializeField] public AbilityType AbilityType { get; private set; }
    }

    [CreateAssetMenu(menuName = "Data/Abilities/" + nameof(ProjectileAbilityParameters), fileName = nameof(ProjectileAbilityParameters))]
    public class ProjectileAbilityParameters : AbilityParameters
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
    
    [CreateAssetMenu(menuName = "Data/Abilities/" + nameof(AoeAbilityParameters), fileName = nameof(AoeAbilityParameters))]
    public class AoeAbilityParameters  : AbilityParameters
    {
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
    }
}