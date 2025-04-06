using UnityEngine;

namespace AbilitiesAssembly.Parameters
{
    [CreateAssetMenu(menuName = "Data/Abilities/" + nameof(ProjectileAbilityParameters), fileName = nameof(ProjectileAbilityParameters))]
    public class ProjectileAbilityParameters : AbilityParameters
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
    

}