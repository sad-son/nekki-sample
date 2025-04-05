using System;
using UnityEngine;

namespace CharacterAssembly.Movement
{
    [Serializable]
    public class MovementSettings
    {
        [field: SerializeField] public MoverType MoverType { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}