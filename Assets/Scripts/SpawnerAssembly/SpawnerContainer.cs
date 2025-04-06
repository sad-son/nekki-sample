using AbilitiesAssembly.Aoe;
using AbilitiesAssembly.Projectiles;
using CharacterAssembly;
using GameplayDependencies;
using ServiceLocatorSystem;
using UnityEngine;

namespace SpawnerAssembly
{
    public class SpawnerContainer : SystemLocatorBase<IGameplayDependency>
    {
        private readonly Character _characterPrefab;
        private readonly Transform _characterSpawnPoint;
        private readonly Transform _enemiesRoot;
        private readonly Transform _projectilesRoot;
        private readonly ConfigsContainer _configsContainer;

        public SpawnerContainer(SpawnDependencies spawnDependencies, ConfigsContainer configsContainer)
        {
            _configsContainer = configsContainer;
            _characterPrefab = spawnDependencies.CharacterPrefab;
            _characterSpawnPoint = spawnDependencies.CharacterSpawnPoint;
            _enemiesRoot = spawnDependencies.EnemyRoot;
            _projectilesRoot = spawnDependencies.ProjectilesRoot;
        }

        protected override void RegisterTypes()
        {
            Register(new MyCharacterDependency(_characterPrefab, _characterSpawnPoint));
            Register(new EnemiesSpawner(_configsContainer.EnemiesConfig, _enemiesRoot));
            Register(new ProjectileSpawner(_configsContainer.ProjectilesConfig, _projectilesRoot));
            Register(new AoeSpawner(_configsContainer.AoeConfig, _projectilesRoot));
        }
    }
}