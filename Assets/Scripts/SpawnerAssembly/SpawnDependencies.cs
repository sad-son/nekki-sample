using System;
using CharacterAssembly;
using UnityEngine;

namespace SpawnerAssembly
{
    [Serializable]
    public class SpawnDependencies
    {
        [field: SerializeField] public Character CharacterPrefab { get; private set; }
        [field: SerializeField] public Transform CharacterSpawnPoint { get; private set; }
        [field: SerializeField] public Transform EnemyRoot { get; private set; }
        [field: SerializeField] public Transform ProjectilesRoot { get; private set; }
    }
}