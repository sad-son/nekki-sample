using System;
using UnityEngine;

namespace CharacterAssembly
{
    [Serializable]
    public class CharacterDependencies
    {
        [field: SerializeField] public Character CharacterPrefab { get; private set; }
        [field: SerializeField] public Transform CharacterSpawnPoint { get; private set; }
        [field: SerializeField] public Transform EnemyRoot { get; private set; }
    }
}