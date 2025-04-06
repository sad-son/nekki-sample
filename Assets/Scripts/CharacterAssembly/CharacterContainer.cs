using ServiceLocatorAssembly;
using SpawnerAssembly;
using UnityEngine;

namespace CharacterAssembly
{
    public class CharacterContainer : SystemLocatorBase<ICharacterDependency>
    {
        private readonly Character _characterPrefab;
        private readonly Transform _characterSpawnPoint;
        private readonly Transform _enemiesRoot;
        private readonly EnemiesConfig _enemiesConfig;

        public CharacterContainer(CharacterDependencies dependencies, EnemiesConfig enemiesConfig)
        {
            _enemiesConfig = enemiesConfig;
            _characterPrefab = dependencies.CharacterPrefab;
            _characterSpawnPoint = dependencies.CharacterSpawnPoint;
            _enemiesRoot = dependencies.EnemyRoot;
        }

        protected override void RegisterTypes()
        {
            Register(new MyCharacterDependency(_characterPrefab, _characterSpawnPoint));
            Register(new EnemiesSpawner(_enemiesConfig, _enemiesRoot));
        }
    }
}