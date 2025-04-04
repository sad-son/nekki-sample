using GameplayDependencies;
using ServiceLocatorSystem;
using UnityEngine;

namespace SpawnerAssembly
{
    public class SpawnerContainer : SystemLocatorBase<IGameplayDependency>
    {
        private readonly GameObject _characterPrefab;
        private readonly Transform _characterSpawnPoint;

        public SpawnerContainer(GameObject characterPrefab, Transform characterSpawnPoint)
        {
            _characterPrefab = characterPrefab;
            _characterSpawnPoint = characterSpawnPoint;
        }

        protected override void RegisterTypes()
        {
            Register(new CharacterSpawner(_characterPrefab, _characterSpawnPoint));
        }
    }
}