using AbilitiesAssembly.Aoe;
using AbilitiesAssembly.Projectiles;
using GameplayDependencies;
using ServiceLocatorAssembly;
using UnityEngine;

namespace SpawnerAssembly
{
    public class SpawnerContainer : SystemLocatorBase<IGameplayDependency>
    {
        private readonly Transform _characterSpawnPoint;
        private readonly Transform _projectilesRoot;
        private readonly ConfigsContainer _configsContainer;

        public SpawnerContainer(SpawnDependencies spawnDependencies, ConfigsContainer configsContainer)
        {
            _configsContainer = configsContainer;
            _projectilesRoot = spawnDependencies.ProjectilesRoot;
        }

        protected override void RegisterTypes()
        {
            Register(new ProjectileSpawner(_configsContainer.ProjectilesConfig, _projectilesRoot));
            Register(new AoeSpawner(_configsContainer.AoeConfig, _projectilesRoot));
        }
    }
}