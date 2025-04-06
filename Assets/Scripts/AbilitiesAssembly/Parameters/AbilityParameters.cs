using UnityEngine;

namespace AbilitiesAssembly.Parameters
{
    public class AbilityParameters : ScriptableObject
    {
        [field: SerializeField] public AbilityType AbilityType { get; private set; }
    }
}