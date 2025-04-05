using GameplayDependencies;
using ServiceLocatorSystem;
using UnityEngine;

namespace SpawnerAssembly
{
    public class SpawnerContainer : SystemLocatorBase<IGameplayDependency>
    {
        private readonly GameObject _characterPrefab;
        private readonly Transform _characterSpawnPoint;
        private readonly EnemiesConfig _enemiesConfig;
        private readonly Transform _enemiesRoot;
        
        public SpawnerContainer(GameObject characterPrefab, Transform characterSpawnPoint, EnemiesConfig enemiesConfig, Transform enemiesRoot)
        {
            _characterPrefab = characterPrefab;
            _characterSpawnPoint = characterSpawnPoint;
            _enemiesConfig = enemiesConfig;
            _enemiesRoot = enemiesRoot;
        }

        protected override void RegisterTypes()
        {
            Register(new MyCharacterDependency(_characterPrefab, _characterSpawnPoint));
            Register(new EnemiesSpawner(_enemiesConfig, _enemiesRoot));
        }
    }
}