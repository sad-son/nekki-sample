using GameplayDependencies;
using UnityEngine;

namespace SpawnerAssembly
{
    public class CharacterSpawner : IGameplayDependency
    {
        private readonly GameObject _characterPrefab;
        private readonly Transform _spawnPoint;

        public CharacterSpawner(GameObject characterPrefab, Transform spawnPoint)
        {
            _characterPrefab = characterPrefab;
            _spawnPoint = spawnPoint;
        }

        public void Spawn()
        {
            Object.Instantiate(_characterPrefab, _spawnPoint.position, _spawnPoint.rotation);
        }
        
        public void Dispose()
        {
            
        }
    }
}