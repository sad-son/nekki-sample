using System;
using UnityEngine;

namespace AbilitiesAssembly
{
    [Serializable]
    public class AbilitiesParameters
    {
        [field: SerializeField] public AbilityType AbilityType { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float LifeTime { get; private set; }
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
    }
}