using System;
using AbilitiesAssembly;
using AbilitiesAssembly.Projectiles;
using SpawnerAssembly;
using UnityEngine;

namespace GameplayDependencies
{
    [Serializable]
    public class ConfigsContainer
    {
        [field: SerializeField] public AbilityConfig AbilityConfig { get; private set; }
        [field: SerializeField] public EnemiesConfig EnemiesConfig { get; private set; }
        [field: SerializeField] public ProjectilesConfig ProjectilesConfig { get; private set; }
    }
}