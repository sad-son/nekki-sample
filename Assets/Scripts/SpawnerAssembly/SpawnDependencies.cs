using System;
using UnityEngine;

namespace SpawnerAssembly
{
    [Serializable]
    public class SpawnDependencies
    {
        [field: SerializeField] public Transform ProjectilesRoot { get; private set; }
    }
}